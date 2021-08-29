using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Bootstrap
{
    public class ErrorResponse : Response
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty("cause")]
        public string Cause { get; set; }

        public ErrorResponse(string error, string message, string cause)
        {
            Error = error;
            ErrorMessage = message;
            Cause = cause;
        }

        public ErrorResponse(string error, string message) : this(error, message, null) { }
    }
}
