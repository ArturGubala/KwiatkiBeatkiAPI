using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/bulk-packs")]
    [ApiController]
    [Authorize]
    public class BulkPacksController : ControllerBase
    {
        private readonly IBulkPacksService _bulkPackService;
        public BulkPacksController(IBulkPacksService bulkPackService)
        {
            _bulkPackService = bulkPackService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _bulkPackService.GetAll();
            return Ok(items);
        }
    }
}
