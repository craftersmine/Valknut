using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace craftersmine.Valknut.Server.Controllers
{
    [Route("valknut/v2/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate()
        {
            return Ok(null);
        }
    }
}
