using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Bootstrap
{
    [Serializable]
    public sealed class BootstrapDataResponse : Response
    {
        public BootstrapData BootstrapData { get; set; }
    }
}
