using Swan.Formatters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Models
{
    [Serializable]
    public class ErrorResponse : Response
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty("cause")]
        public string Cause { get; set; }

        public ErrorResponse() { }

        public ErrorResponse(string error, string message, string cause)
        {
            Error = error;
            ErrorMessage = message;
            Cause = cause;
        }

        public ErrorResponse(string error, string message) : this(error, message, null) { }
    }
}
