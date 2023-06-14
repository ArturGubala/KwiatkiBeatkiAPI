using KwiatkiBeatkiAPI.Models.Document;
using KwiatkiBeatkiAPI.Models.Item;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/v1/documents")]
    [ApiController]
    [Authorize]
    public class DocumentsController : ControllerBase
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
        
        [HttpPost]
        [Authorize(Roles = "Admin,Menager")]
        public async Task<IActionResult> Post([FromBody] CreateDocumentDto createDocumentDto)
        {
            var createdDocumentId = await _documentsService.CreateAsync(createDocumentDto);
            return Created($"api/v1/documents/{createdDocumentId}", null);
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
