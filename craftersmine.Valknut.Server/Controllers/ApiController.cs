using craftersmine.Valknut.Server.Models;
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
using System.Xml.Serialization;

namespace craftersmine.Valknut.Server.Controllers
{
    public sealed class ApiController : WebApiController
    {
        [Route(HttpVerbs.Post, "/profile/uploadSkin")]
        public async Task<Response> UploadSkin([QueryField(true)] string userId)
        {
            var authorizationHeader = Request.Headers.GetValues("Authorization");
            string uuid;
            if (authorizationHeader is not null)
            {
                var authHead = authorizationHeader[0].Split(' ');
                if (authHead.Length > 1)
                {
                    if (authHead[0] == "Bearer")
                        uuid = JwtHelper.ValidateJwtToken(authHead[1]);
                    else
                    {
                        Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return new ErrorResponse("ForbiddenOperationException", "Invalid authorization header. Required: Bearer. Got: " + authHead[0]);
                    }
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return new ErrorResponse("ForbiddenOperationException", "Invalid authorization header");
                }
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "No authorization header passed!");
            }

            if (string.IsNullOrWhiteSpace(uuid))
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid authorization token");
            }

            var parser = await MultipartFormDataParser.ParseAsync(Request.InputStream);

            var path = Path.Combine(Program.Config.PathsConfig.ContentPath, "skins", uuid + ".png");

            FilePart imgData = null;
            if (parser.Files.Count >= 1)
                imgData = parser.Files[0];
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ErrorResponse("NoFileException", "No uploaded image in POST form-multipart data");
            }

            using (Image skinImage = Image.Load(imgData.Data))
            {
                if (skinImage.Height != 32 && skinImage.Width != 64)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return new ErrorResponse("InvalidImageSizeException", "Uploaded image size invalid! Must be 64x32 or 64x64! Got " + skinImage.Width + "x" + skinImage.Height);
                }
                await skinImage.SaveAsPngAsync(path);
                Response.StatusCode = (int)HttpStatusCode.NoContent;
                return null;
            }
        }

        [Route(HttpVerbs.Post, "/profile/uploadCape")]
        public async Task<Response> UploadCape([QueryField(true)] string userId)
        {
            if (!Program.Config.PathsConfig.EnableCapeUpload)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Server disabled cape uploading");
            }

            var authorizationHeader = Request.Headers.GetValues("Authorization");
            string uuid;
            if (authorizationHeader is not null)
            {
                var authHead = authorizationHeader[0].Split(' ');
                if (authHead.Length > 1)
                {
                    if (authHead[0] == "Bearer")
                        uuid = JwtHelper.ValidateJwtToken(authHead[1]);
                    else
                    {
                        Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return new ErrorResponse("ForbiddenOperationException", "Invalid authorization header. Required: Bearer. Got: " + authHead[0]);
                    }
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return new ErrorResponse("ForbiddenOperationException", "Invalid authorization header");
                }
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "No authorization header passed!");
            }

            if (string.IsNullOrWhiteSpace(uuid))
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ErrorResponse("ForbiddenOperationException", "Invalid authorization token");
            }

            var parser = await MultipartFormDataParser.ParseAsync(Request.InputStream);

            var path = Path.Combine(Program.Config.PathsConfig.ContentPath, "capes", uuid + ".png");

            FilePart imgData = null;
            if (parser.Files.Count >= 1)
                imgData = parser.Files[0];
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ErrorResponse("NoFileException", "No uploaded image in POST form-multipart data");
            }

            using (Image capeImage = Image.Load(imgData.Data))
            {
                if (!(capeImage.Height == 32) && !(capeImage.Width == 64))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return new ErrorResponse("InvalidImageSizeException", "Uploaded image size invalid! Must be 64x32! Got " + capeImage.Width + "x" + capeImage.Height);
                }
                await capeImage.SaveAsPngAsync(path);
                Response.StatusCode = (int)HttpStatusCode.NoContent;
                return null;
            }
        }

        [Route(HttpVerbs.Get, "/getClients")]
        public Response GetClients()
        {
            var clientsMeta = ClientsHelper.GetMinecraftClientsWithoutFiles();

            return new GetClientsResponse() { Clients = clientsMeta };
        }

        [Route(HttpVerbs.Get, "/getClient")]
        public Response GetClientData([QueryField(true)] string clientId)
        {
            var client = ClientsHelper.GetMinecraftClient(clientId);
            if (client is null)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ErrorResponse("ClientMetadataCollectionException", "Unable to collect all required information about client from server! Contact server system administrator!");
            }

            return new GetClientDataResponse() { Client = client };
        }

        [Route(HttpVerbs.Get, "/getBootstrapData")]
        public string GetBootstrapData()
        {
            var bootstrapData = BootstrapHelper.GetBootstrapData();
            if (bootstrapData is not null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BootstrapData));
                using (StringWriter writer = new StringWriter())
                {
                    serializer.Serialize(writer, bootstrapData);
                    return writer.ToString();
                }
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ErrorResponse));
                using (StringWriter writer = new StringWriter())
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    serializer.Serialize(writer, new ErrorResponse("BootstrapException", "Unable to get bootstrap data"));
                    return writer.ToString();
                }
            }
        }
    }
}
