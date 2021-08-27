using craftersmine.Valknut.Launcher.Authentication.Models;
using craftersmine.Valknut.Launcher.Authentication.Models.Requests;
using craftersmine.Valknut.Launcher.Authentication.Models.Responses;

using Newtonsoft.Json;

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
    }
}
