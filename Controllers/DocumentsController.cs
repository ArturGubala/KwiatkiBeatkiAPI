﻿using KwiatkiBeatkiAPI.Models.Document;
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
        [Authorize(Roles = "Admin,Menager")]
        public IActionResult Post([FromBody] CreateDocumentDto createDocumentDto)
        {
            var createdDocumentId = _documentsService.CreateDocument(createDocumentDto);
            return Created($"api/v1/documents/{createdDocumentId}", null);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        public IActionResult Delete([FromRoute] int id)
        {
            _documentsService.DeleteDocument(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateDocumentDto updateDocumentDto)
        {
            _documentsService.UpdateDocument(id, updateDocumentDto);
            return NoContent();
        }
    }
}
