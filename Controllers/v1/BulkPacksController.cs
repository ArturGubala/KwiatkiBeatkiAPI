using KwiatkiBeatkiAPI.Models.BulkPack;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [ProducesResponseType(typeof(IEnumerable<BulkPackDto>), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Get()
        {
            var bulkPackDtos = await _bulkPackService.GetAsync();
            return Ok(bulkPackDtos);
        }
    }
}
