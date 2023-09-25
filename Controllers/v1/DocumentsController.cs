using KwiatkiBeatkiAPI.Models.Document;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KwiatkiBeatkiAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/documents")]
    public class DocumentsController : ApiController
    {
        private readonly IDocumentsService _documentsService;
        public DocumentsController(IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DocumentDto>), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Get()
        {
            var documentDtos = await _documentsService.GetAsync();
            return Ok(documentDtos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DocumentDto), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var documentDto = await _documentsService.GetAsync(id);
            return Ok(documentDto);
        }

        [HttpGet("{id:int}/generete-document")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        public async Task<IActionResult> GetByte([FromRoute] int id)
        {
            var documentByteArray = await _documentsService.GenerateDocument(id);
            return File(documentByteArray, "application/pdf");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Menager")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Post([FromBody] CreateDocumentDto createDocumentDto)
        {
            var createdDocumentId = await _documentsService.CreateAsync(createDocumentDto);
            var createdResourceUrl = Url.Action(nameof(Get), new { id = createdDocumentId });
            return Created(createdResourceUrl!, null);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _documentsService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateDocumentDto updateDocumentDto)
        {
            await _documentsService.UpdateAsync(id, updateDocumentDto);
            return NoContent();
        }
    }
}
