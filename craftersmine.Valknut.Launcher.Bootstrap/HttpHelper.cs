using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace craftersmine.Valknut.Launcher.Bootstrap
{
    public sealed class HttpHelper
    {
        public static async Task<Response> MakePostRequest(string uri, string value, string accessToken = "")
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(value);
                if (!string.IsNullOrWhiteSpace(accessToken))
                    content.Headers.Add("Authorization", "Bearer " + accessToken);
                var response = await client.PostAsync(uri, content);
                string respVal = await response.Content.ReadAsStringAsync();
                return new Response() { ResponseData = respVal, StatusCode = response.StatusCode, IsSuccessful = response.IsSuccessStatusCode };
            }
        }
        public static async Task<Response> MakePostRequest(string uri, byte[] file, string accessToken = "")
        {
            using (HttpClient client = new HttpClient())
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add(new ByteArrayContent(file), "file");
                if (!string.IsNullOrWhiteSpace(accessToken))
                    content.Headers.Add("Authorization", "Bearer " + accessToken);
                var response = await client.PostAsync(uri, content);
                string respVal = await response.Content.ReadAsStringAsync();
                return new Response() { ResponseData = respVal, StatusCode = response.StatusCode, IsSuccessful = response.IsSuccessStatusCode };
            }
        }

        public static async Task<Response> MakeGetRequest(string uri, Dictionary<string, string> values, string accessToken = "")
        {
            using (HttpClient client = new HttpClient())
            {
                List<string> args = new List<string>();
                foreach (var arg in values)
                {
                    args.Add(arg.Key + "=" + arg.Value);
                }
                string argsUriEncoded = string.Join("&", args.ToArray());
                if (!string.IsNullOrWhiteSpace(accessToken))
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync(HttpUtility.UrlEncode(uri + "?" + argsUriEncoded));
                string respVal = await response.Content.ReadAsStringAsync();
                return new Response() { ResponseData = respVal, StatusCode = response.StatusCode, IsSuccessful = response.IsSuccessStatusCode };
            }
        }
    }
}
