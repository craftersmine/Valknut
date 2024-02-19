using craftersmine.Valknut.Launcher.Authentication.Models;
using craftersmine.Valknut.Launcher.Authentication.Models.Requests;
using craftersmine.Valknut.Launcher.Authentication.Models.Responses;

using Newtonsoft.Json;
using Swan.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Authentication
{
    public sealed class Authenticator
    {
        public static async Task<Response> Authenticate(string email, string password)
        {
            string uri = LauncherSettings.GetServerAddress() + "auth/authenticate";

            Logger.Info("Authenticating user at " + uri);

            var authenticationRequest = new AuthenticationRequest()
            {
                Agent = new Agent() { Name = "Minecraft", Version = 1 },
                ClientToken = null,
                Username = email,
                Password = password,
                RequestUser = false
            };

            var requestValue = JsonConvert.SerializeObject(authenticationRequest);

            var response = await HttpHelper.MakePostRequest(uri, requestValue);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<AuthenticationResponse>(response.ResponseData);
            else return JsonConvert.DeserializeObject<ErrorResponse>(response.ResponseData);
        }

        public static async Task<bool> ValidateUser(string accessToken, string clientToken)
        {
            string uri = LauncherSettings.GetServerAddress() + "auth/validate";

            Logger.Info("Validating user access token at " + uri);

            var validationRequest = new RefreshValidateTokenRequest()
            {
                AccessToken = accessToken,
                ClientToken = clientToken
            };

            var requestValue = JsonConvert.SerializeObject(validationRequest);

            var response = await HttpHelper.MakePostRequest(uri, requestValue).ConfigureAwait(false);

            if (response.IsSuccessful)
                return true;
            else return false;
        }
    }
}
