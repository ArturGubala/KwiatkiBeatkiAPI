using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/v1/bulk-packs")]
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
        public async Task<IActionResult> Get()
        {
            var bulkPackDtos = await _bulkPackService.GetAsync();
            return Ok(bulkPackDtos);
        }
    }
}
