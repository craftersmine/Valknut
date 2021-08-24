using craftersmine.Valknut.Server.Database;
using craftersmine.Valknut.Server.Models;
using craftersmine.Valknut.Server.Models.Requests;
using craftersmine.Valknut.Server.Models.Responses;

using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;

using HttpMultipartParser;

using SixLabors.ImageSharp;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Controllers
{
    public sealed class GameSessionController : WebApiController
    {
        [Route(HttpVerbs.Post, "/join")]
        public Response Join()
        {
            var sessionJoinRequest = HttpContext.GetRequestDataAsync<SessionJoinRequest>().Result;

            var uuid = JwtHelper.ValidateJwtToken(sessionJoinRequest.AccessToken);
            if (uuid is null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid access token");
            }

            var userAccount = AccountsTableHelper.GetUserAccountByUuid(uuid);

            SessionsTableHelper.UpdateUserSession(userAccount.Username, uuid, serverId: sessionJoinRequest.ServerId);
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            return null;
        }

        [Route(HttpVerbs.Get, "/hasJoined")]
        public Response HasJoined([QueryField] string username, [QueryField] string serverId, [QueryField(false)] string ip)
        {
            SessionJoinResponse sessionJoinResponse = new SessionJoinResponse();
            var userSession = SessionsTableHelper.GetUserSessionByServerId(serverId);

            sessionJoinResponse.Id = userSession.Uuid;
            sessionJoinResponse.Name = username;
            UserProperties properties = new UserProperties();

            UserTexture skin = null;
            if (File.Exists(Path.Combine(Program.Config.PathsConfig.ContentPath, "skins", userSession.Uuid + ".png")))
            {
                string prot = "http";
                if (Program.IsHttps())
                    prot = "https";
                if (Program.Config.WebServerConfig.Port == 80)
                    skin = new UserTexture() { Url = prot + "://" + Program.Config.WebServerConfig.BindAddress + "/valknut/textures/skins/" + userSession.Uuid + ".png" };
                else
                    skin = new UserTexture() { Url = prot + "://" + Program.Config.WebServerConfig.BindAddress + ":" + Program.Config.WebServerConfig.Port + "/valknut/textures/skins/" + userSession.Uuid + ".png" };
            }
            UserTexture cape = null;
            if (File.Exists(Path.Combine(Program.Config.PathsConfig.ContentPath, "skins", userSession.Uuid + ".png")))
            {
                string prot = "http";
                if (Program.IsHttps())
                    prot = "https";
                if (Program.Config.WebServerConfig.Port == 80)
                    cape = new UserTexture() { Url = prot + "://" + Program.Config.WebServerConfig.BindAddress + "/valknut/textures/capes/" + userSession.Uuid + ".png" };
                else
                    cape = new UserTexture() { Url = prot + "://" + Program.Config.WebServerConfig.BindAddress + ":" + Program.Config.WebServerConfig.Port + "/valknut/textures/capes/" + userSession.Uuid + ".png" };
            }

            properties.Name = "textures";
            string value =
                "{\r\n" +
                "\"timestamp\": %ts%,\r\n" +
                "\"profileId\": \"%profileId%\",\r\n" +
                "\"profileName\": \"%profileName%\",\r\n" +
                "\"signatureRequired\": false,\r\n" +
                "\"textures\": \r\n{ %textures%\r\n }\r\n" +
                "}";
            string skinVal = "\r\n\"SKIN\": { \"url\": \"%url%\" }";
            string capeVal = "\r\n\"CAPE\": { \"url\": \"%url%\" }";

            value = value
                .Replace("%ts%", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
                .Replace("%profileId%", userSession.Uuid)
                .Replace("%profileName%", userSession.Username);

            string skinCapeCtor = "";
            bool isSkinAdded = false;

            if (skin is not null)
            {
                skinVal = skinVal.Replace("%url%", skin.Url);
                skinCapeCtor += skinCapeCtor;
                isSkinAdded = true;
            }

            if (cape is not null)
            {
                capeVal = capeVal.Replace("%url%", cape.Url);
                if (isSkinAdded)
                    skinCapeCtor += ", " + capeVal;
                else skinCapeCtor += capeVal;
            }

            value = value.Replace("%textures%", skinCapeCtor);

            properties.Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
            properties.Signature = "Cg==";

            sessionJoinResponse.Properties = new[] { properties };
            return sessionJoinResponse;
        }

        [Route(HttpVerbs.Get, "/profile/{uuid}")]
        public Response Profile(string uuid)
        {
            var userAccount = AccountsTableHelper.GetUserAccountByUuid(uuid);
            SessionJoinResponse sessionJoinResponse = new SessionJoinResponse();
            sessionJoinResponse.Id = userAccount.Uuid;
            sessionJoinResponse.Name = userAccount.Username;

            UserProperties properties = new UserProperties();

            UserTexture skin = null;
            if (File.Exists(Path.Combine(Program.Config.PathsConfig.ContentPath, "skins", userAccount.Uuid + ".png")))
            {
                string prot = "http";
                if (Program.IsHttps())
                    prot = "https";
                if (Program.Config.WebServerConfig.Port == 80)
                    skin = new UserTexture() { Url = prot + "://" + Program.Config.WebServerConfig.BindAddress + "/valknut/textures/skins/" + userAccount.Uuid + ".png" };
                else
                    skin = new UserTexture() { Url = prot + "://" + Program.Config.WebServerConfig.BindAddress + ":" + Program.Config.WebServerConfig.Port + "/valknut/textures/skins/" + userAccount.Uuid + ".png" };
            }
            UserTexture cape = null;
            if (File.Exists(Path.Combine(Program.Config.PathsConfig.ContentPath, "skins", userAccount.Uuid + ".png")))
            {
                string prot = "http";
                if (Program.IsHttps())
                    prot = "https";
                if (Program.Config.WebServerConfig.Port == 80)
                    cape = new UserTexture() { Url = prot + "://" + Program.Config.WebServerConfig.BindAddress + "/valknut/textures/capes/" + userAccount.Uuid + ".png" };
                else
                    cape = new UserTexture() { Url = prot + "://" + Program.Config.WebServerConfig.BindAddress + ":" + Program.Config.WebServerConfig.Port + "/valknut/textures/capes/" + userAccount.Uuid + ".png" };
            }

            properties.Name = "textures";
            string value =
                "{\r\n" +
                "\"timestamp\": %ts%,\r\n" +
                "\"profileId\": \"%profileId%\",\r\n" +
                "\"profileName\": \"%profileName%\",\r\n" +
                "\"signatureRequired\": false,\r\n" +
                "\"textures\": \r\n{ %textures%\r\n }\r\n" +
                "}";
            string skinVal = "\r\n\"SKIN\": { \"url\": \"%url%\" }";
            string capeVal = "\r\n\"CAPE\": { \"url\": \"%url%\" }";

            value = value
                .Replace("%ts%", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
                .Replace("%profileId%", userAccount.Uuid)
                .Replace("%profileName%", userAccount.Username);

            string skinCapeCtor = "";
            bool isSkinAdded = false;

            if (skin is not null)
            {
                skinVal = skinVal.Replace("%url%", skin.Url);
                skinCapeCtor += skinVal;
                isSkinAdded = true;
            }

            if (cape is not null)
            {
                capeVal = capeVal.Replace("%url%", cape.Url);
                if (isSkinAdded)
                    skinCapeCtor += ", " + capeVal;
                else skinCapeCtor += capeVal;
            }
            if (skin is not null || cape is not null)
                value = value.Replace("%textures%", skinCapeCtor);
            else value = value.Replace("%textures%", "");

            properties.Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
            properties.Signature = "Cg==";

            sessionJoinResponse.Properties = new[] { properties };
            return sessionJoinResponse;
        }
    }
}
