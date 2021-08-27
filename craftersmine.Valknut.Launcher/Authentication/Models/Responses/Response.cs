using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Authentication.Models
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseData { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
