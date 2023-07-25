using KwiatkiBeatkiAPI.Models.Document;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Get()
        {
            var documentDtos = await _documentsService.GetAsync();
            return Ok(documentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var documentDto = await _documentsService.GetAsync(id);
            return Ok(documentDto);
        }

        [HttpGet("{id:int}/generete-document")]
        public async Task<IActionResult> GetByte([FromRoute] int id)
        {
            var documentByteArray = await _documentsService.GenerateDocument(id);
            return File(documentByteArray, "application/pdf");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Menager")]
        public async Task<IActionResult> Post([FromBody] CreateDocumentDto createDocumentDto)
        {
            var createdDocumentId = await _documentsService.CreateAsync(createDocumentDto);
            var createdResourceUrl = Url.Action(nameof(Get), new { id = createdDocumentId });
            return Created(createdResourceUrl!, null);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _documentsService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateDocumentDto updateDocumentDto)
        {
            await _documentsService.UpdateAsync(id, updateDocumentDto);
            return NoContent();
        }
    }
}
