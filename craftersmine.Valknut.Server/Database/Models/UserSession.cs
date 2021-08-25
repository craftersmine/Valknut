using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Database.Models
{
    public sealed class UserSession
    {
        public string Uuid { get; set; }
        public string Username { get; set; }
        public string ServerId { get; set; }
        public string SessionId { get; set; }
        public string ClientToken { get; set; }
    }
}
