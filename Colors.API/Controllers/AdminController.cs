using System.Runtime.InteropServices;

namespace Colors.API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "v3")]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    [HttpGet("sys-info")]
    public ActionResult GetSystemInformation()
    {
        return Ok(new { os = Environment.OSVersion.VersionString, net = RuntimeInformation.FrameworkDescription });
    }
}