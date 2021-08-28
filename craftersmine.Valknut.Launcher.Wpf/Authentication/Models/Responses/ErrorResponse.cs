﻿using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Authentication.Models
{
    public class ErrorResponse : Response
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty("cause")]
        public string Cause { get; set; }
        public ErrorResponse()
        { }

        public ErrorResponse(string error, string message, string cause)
        {
            Error = error;
            ErrorMessage = message;
            Cause = cause;
        }

        public string GetLocalizedMessage()
        {
            return ""; // TODO: Implement getting localized message for error
        }

        public ErrorResponse(string error, string message) : this(error, message, null) { }
    }
}
