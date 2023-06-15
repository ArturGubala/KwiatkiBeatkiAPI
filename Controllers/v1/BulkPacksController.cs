using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/bulk-packs")]
    public class BulkPacksController : ApiController
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
