using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Bootstrap
{
    [Serializable]
    public sealed class BootstrapData : Response
    {
        public string Id { get; set; }
        public string Version { get; set; }
        public string Hash { get; set; }
        public string Archive { get; set; }
    }
}
