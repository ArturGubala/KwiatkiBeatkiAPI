using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ApiController : ControllerBase
    {
    }
}
