using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Bootstrap
{
    [Serializable]
    public class ErrorResponse : Response
    {
        public string Error { get; set; }
        public string ErrorMessage { get; set; }
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
