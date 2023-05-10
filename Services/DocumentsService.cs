﻿using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.Document;
using KwiatkiBeatkiAPI.Entities.Line;
using KwiatkiBeatkiAPI.Models.Document;
using KwiatkiBeatkiAPI.Exeptions;
using Microsoft.EntityFrameworkCore;
using KwiatkiBeatkiAPI.Entities.Item;
using KwiatkiBeatkiAPI.Models.Item;
using System.Reflection.Metadata;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IDocumentsService
    {
        IEnumerable<DocumentDto> GetAll();
        DocumentDto GetById(int documentId);
        int CreateDocument(CreateDocumentDto createDocumentDto);
    }
    public class DocumentsService : IDocumentsService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public DocumentsService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IMapper mapper, IUserContextService userContextService)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public IEnumerable<DocumentDto> GetAll()
        {
            var documentEntities = _kwiatkiBeatkiDbContext.Document
                .Include(i => i.DocumentType)
                .Include(i => i.WarehouseFrom)
                .Include(i => i.WarehouseTo)
                .Include(i => i.TradePartner)
                .Include(i => i.User)
                    .ThenInclude(i => i.Role)
                .Include(i => i.Lines)
                    .ThenInclude(i => i.Item)
                        .ThenInclude(i => i.MeasurementUnit)
                .ToList();

            var documentDtos = _mapper.Map<IEnumerable<DocumentDto>>(documentEntities);

            return documentDtos;
        }

        public DocumentDto GetById(int documentId)
        {
            var documentEntity = _kwiatkiBeatkiDbContext.Document
                .Include(i => i.DocumentType)
                .Include(i => i.WarehouseFrom)
                .Include(i => i.WarehouseTo)
                .Include(i => i.TradePartner)
                .Include(i => i.User)
                    .ThenInclude(i => i.Role)
                .Include(i => i.Lines)
                    .ThenInclude(i => i.Item)
                        .ThenInclude(i => i.MeasurementUnit)
                .FirstOrDefault(d => d.Id == documentId);

            if (documentEntity is null)
                throw new NotFoundException("Document not found");

            var documentDto = _mapper.Map<DocumentDto>(documentEntity);

            return documentDto;
        }

        public int CreateDocument(CreateDocumentDto createDocumentDto)
        {
            var documentEntity = PrepareDocumentEntity(createDocumentDto);
            var lineEntities = _mapper.Map<IEnumerable<LineEntity>>(createDocumentDto.Lines);

            _kwiatkiBeatkiDbContext.Database.BeginTransaction();

            try
            {
                _kwiatkiBeatkiDbContext.Document.Add(documentEntity);
                _kwiatkiBeatkiDbContext.SaveChanges();

                foreach (var lineEntity in lineEntities)
                    lineEntity.DocumentId = documentEntity.Id;

                _kwiatkiBeatkiDbContext.Line.AddRange(lineEntities);
                _kwiatkiBeatkiDbContext.SaveChanges();
                _kwiatkiBeatkiDbContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                _kwiatkiBeatkiDbContext.Database.RollbackTransaction();
                throw new BadRequestException("Error while adding document", ex);
            }

            return documentEntity.Id;
        }
        private DocumentEntity PrepareDocumentEntity(CreateDocumentDto createDocumentDto)
        {
            var documentEntity = _mapper.Map<DocumentEntity>(createDocumentDto);
            var lastDocumentNumber = GetLastDocumentNumber(createDocumentDto.DocumentTypeId);
            var documentTypeAbbreviation = GetDocumentTypeAbbreviation(createDocumentDto.DocumentTypeId);
            var currentDocumentNumber = lastDocumentNumber + 1;
            var currentFullDocumentNumber = $"{currentDocumentNumber:D2}/{documentTypeAbbreviation}/{documentEntity.Created.ToString("MM")}/{documentEntity.Created.Year}";

            documentEntity.DocumentNumber = currentDocumentNumber;
            documentEntity.FullDocumentNumber = currentFullDocumentNumber;
            documentEntity.UserId = (int)_userContextService.UserId;

            return documentEntity;
        }

        private int GetLastDocumentNumber(int documentTypeId)
        {
            var currentDateTime = DateTime.Now;
            var lastDocumentNumber = _kwiatkiBeatkiDbContext.Document
                .Where(d => d.DocumentTypeId == documentTypeId && d.Created.Month == currentDateTime.Month && d.Created.Year == currentDateTime.Year)
                .Select(d => d.DocumentNumber)
                .DefaultIfEmpty()
                .Max();

            return lastDocumentNumber;
        }

        private string GetDocumentTypeAbbreviation(int documentTypeId)
        {
            var documentTypeAbbreviation = _kwiatkiBeatkiDbContext.DocumentType.Where(d => d.Id == documentTypeId).Select(d => d.Abbreviation).First();

            return documentTypeAbbreviation;
        }


    }
}