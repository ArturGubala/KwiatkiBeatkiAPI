using KwiatkiBeatkiAPI.Models.Document;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/documents")]
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
        public IActionResult Get()
        {
            var documentDtos = _documentsService.GetAll();
            return Ok(documentDtos);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id) 
        { 
            var documentDto = _documentsService.GetById(id);
            return Ok(documentDto);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] CreateDocumentDto createDocumentDto)
        {
            var createdDocumentId = _documentsService.CreateDocument(createDocumentDto);
            return Created($"api/documents/{createdDocumentId}", null);
        }
    }
}
