using BCrypt.Net;

using craftersmine.Valknut.Server.Database;
using craftersmine.Valknut.Server.Database.Models;
using craftersmine.Valknut.Server.Models;
using craftersmine.Valknut.Server.Models.Requests;
using craftersmine.Valknut.Server.Models.Responses;

using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;

using Org.BouncyCastle.Crypto.Generators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Controllers
{
    public sealed class AuthenticationController : WebApiController
    {
        [Route(HttpVerbs.Post, "/authenticate")]
        public Response Authenticate()
        {
            var authenticationRequest = HttpContext.GetRequestDataAsync<AuthenticationRequest>().Result;

            string clientToken = authenticationRequest.ClientToken;

            UserAccount userAccount = AccountsTableHelper.GetUserAccountByEmail(authenticationRequest.Username);
            if (userAccount is null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid username");
            }

            if (!BCrypt.Net.BCrypt.Verify(authenticationRequest.Password, userAccount.Password))
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid password");
            }

            if (string.IsNullOrWhiteSpace(authenticationRequest.ClientToken))
                clientToken = Guid.NewGuid().ToString().Replace("-", "");

            var authenticationResponse = new AuthenticationResponse();
            string sessionId = SessionsTableHelper.GenerateSessionId(userAccount.Username, clientToken);
            authenticationResponse.ClientToken = clientToken;
            authenticationResponse.AvailableProfiles = new UserProfile[] { new UserProfile() { Id = userAccount.Uuid, Name = userAccount.Username } };
            authenticationResponse.SelectedProfile = new UserProfile() { Id = userAccount.Uuid, Name = userAccount.Username };
            authenticationResponse.AccessToken = JwtHelper.GenerateJwtToken(userAccount.Username, userAccount.Uuid, sessionId);
            SessionsTableHelper.UpdateUserSession(userAccount.Username, userAccount.Uuid, sessionId, clientToken: clientToken);

            return authenticationResponse;
        }

        [Route(HttpVerbs.Post, "/refresh")]
        public Response Refresh()
        {
            var refreshTokenRequest = HttpContext.GetRequestDataAsync<RefreshValidateTokenRequest>().Result;

            var uuid = JwtHelper.ValidateJwtToken(refreshTokenRequest.AccessToken);
            if (uuid is null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid access token");
            }

            var lastClientToken = SessionsTableHelper.GetUserClientToken(uuid);
            if (lastClientToken != refreshTokenRequest.ClientToken)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid client token");
            }

            var userAcc = AccountsTableHelper.GetUserAccountByUuid(uuid);

            string sessionId = SessionsTableHelper.GenerateSessionId(userAcc.Username, uuid);

            RefreshTokenResponse refreshTokenResponse = new RefreshTokenResponse();
            refreshTokenResponse.AccessToken = JwtHelper.GenerateJwtToken(userAcc.Username, uuid, sessionId);
            refreshTokenResponse.ClientToken = refreshTokenRequest.ClientToken;
            refreshTokenResponse.SelectedProfile = new UserProfile() { Name = userAcc.Username, Id = userAcc.Uuid };

            SessionsTableHelper.UpdateUserSession(userAcc.Username, userAcc.Uuid, sessionId, clientToken: refreshTokenRequest.ClientToken);

            return refreshTokenResponse;
        }

        [Route(HttpVerbs.Post, "/validate")]
        public Response Validate()
        {
            var validateTokenRequest = HttpContext.GetRequestDataAsync<RefreshValidateTokenRequest>().Result;

            var uuid = JwtHelper.ValidateJwtToken(validateTokenRequest.AccessToken);
            if (uuid is null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid access token");
            }

            var lastClientToken = SessionsTableHelper.GetUserClientToken(uuid);
            if (lastClientToken != validateTokenRequest.ClientToken)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid client token");
            }

            HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            return null;
        }

        [Route(HttpVerbs.Post, "/signout")]
        public Response SignOut()
        {
            var signOutRequest = HttpContext.GetRequestDataAsync<SignOutRequest>().Result;

            var userAccount = AccountsTableHelper.GetUserAccountByEmail(signOutRequest.Username);

            if (userAccount is null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid username");
            }

            if (!BCrypt.Net.BCrypt.Verify(signOutRequest.Password, userAccount.Password))
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid password");
            }

            SessionsTableHelper.UpdateUserSession(userAccount.Username, userAccount.Uuid);
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            return null;
        }

        [Route(HttpVerbs.Post, "/invalidate")]
        public Response Invalidate()
        {
            var invalidateTokenRequest = HttpContext.GetRequestDataAsync<RefreshValidateTokenRequest>().Result;

            var uuid = JwtHelper.ValidateJwtToken(invalidateTokenRequest.AccessToken);
            if (uuid is null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid access token");
            }

            var lastClientToken = SessionsTableHelper.GetUserClientToken(uuid);
            if (lastClientToken != invalidateTokenRequest.ClientToken)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid client token");
            }

            var userAccount = AccountsTableHelper.GetUserAccountByUuid(uuid);
            string invalidatedSessionId = SessionsTableHelper.GenerateSessionId(Program.Config.SecurityConfig.Secret, Guid.NewGuid().ToString());
            SessionsTableHelper.UpdateUserSession(userAccount.Username, userAccount.Uuid, invalidatedSessionId, "", "");
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            return null;
        }

        [Route(HttpVerbs.Post, "/register")]
        public Response Register()
        {
            var registerRequest = HttpContext.GetRequestDataAsync<RegisterRequest>().Result;

            var account = AccountsTableHelper.GetUserAccount(registerRequest.Username);
            if (account is not null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ErrorResponse("RegisterUserExistsException", "Account with same name exists!");
            }

            account = AccountsTableHelper.GetUserAccountByEmail(registerRequest.Email);
            if (account is not null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ErrorResponse("RegisterEmailExistsException", "Account with same email exists!");
            }

            var uuid = Guid.NewGuid().ToString().Replace("-", "");

            if (registerRequest.Password != registerRequest.RepeatedPassword)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ErrorResponse("RegisterPasswordsNotExactException", "Passwords not exact!");
            }

            var encrypedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
            bool isUserRegistered = AccountsTableHelper.CreateUserAccount(registerRequest.Username, registerRequest.Email, encrypedPassword, uuid);

            if (!isUserRegistered)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ErrorResponse("RegisterException", "Unable to register user due to unknown error!");
            }


            Response.StatusCode = (int)HttpStatusCode.NoContent;
            return null;
        }
    }
}
