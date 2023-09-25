using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.Document;
using KwiatkiBeatkiAPI.Entities.Line;
using KwiatkiBeatkiAPI.Models.Document;
using KwiatkiBeatkiAPI.Exeptions;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using KwiatkiBeatkiAPI.Services.ComposeDocument;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IDocumentsService
    {
        Task<IEnumerable<DocumentDto>> GetAsync();
        Task<DocumentDto> GetAsync(int documentId);
        Task<int> CreateAsync(CreateDocumentDto createDocumentDto);
        Task UpdateAsync(int id, UpdateDocumentDto updateDocumentDto);
        Task DeleteAsync(int documentId);
        Task<byte[]> GenerateDocument(int documentId);
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
        public async Task<IEnumerable<DocumentDto>> GetAsync()
        {
            var documentEntities = await _kwiatkiBeatkiDbContext.Document
                .Include(i => i.DocumentType)
                .Include(i => i.WarehouseFrom)
                .Include(i => i.WarehouseTo)
                .Include(i => i.TradePartner)
                .Include(i => i.User)
                    .ThenInclude(i => i.Role)
                .Include(i => i.Lines)
                    .ThenInclude(i => i.Item)
                        .ThenInclude(i => i.MeasurementUnit)
                .ToListAsync();

            var documentDtos = _mapper.Map<IEnumerable<DocumentDto>>(documentEntities);

            return documentDtos;
        }

        public async Task<DocumentDto> GetAsync(int documentId)
        {
            var documentEntity = await _kwiatkiBeatkiDbContext.Document
                .Include(i => i.DocumentType)
                .Include(i => i.WarehouseFrom)
                .Include(i => i.WarehouseTo)
                .Include(i => i.TradePartner)
                .Include(i => i.User)
                    .ThenInclude(i => i.Role)
                .Include(i => i.Lines)
                    .ThenInclude(i => i.Item)
                        .ThenInclude(i => i.MeasurementUnit)
                .FirstOrDefaultAsync(d => d.Id == documentId);

            if (documentEntity is null)
                throw new NotFoundException("DocumentId", $"Document with ID: {documentId} was not found");

            var documentDto = _mapper.Map<DocumentDto>(documentEntity);

            return documentDto;
        }

        public async Task<int> CreateAsync(CreateDocumentDto createDocumentDto)
        {
            var documentEntity = await PrepareDocumentEntityAsync(createDocumentDto);
            var lineEntities = _mapper.Map<IEnumerable<LineEntity>>(createDocumentDto.Lines);

            await _kwiatkiBeatkiDbContext.Database.BeginTransactionAsync();

            try
            {
                await _kwiatkiBeatkiDbContext.Document.AddAsync(documentEntity);
                await _kwiatkiBeatkiDbContext.SaveChangesAsync();

                foreach (var lineEntity in lineEntities)
                    lineEntity.DocumentId = documentEntity.Id;

                await _kwiatkiBeatkiDbContext.Line.AddRangeAsync(lineEntities);
                await _kwiatkiBeatkiDbContext.SaveChangesAsync();
                await _kwiatkiBeatkiDbContext.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _kwiatkiBeatkiDbContext.Database.RollbackTransactionAsync();
                throw new BadRequestException("Document", "Error while adding document", ex);
            }

            return documentEntity.Id;
        }
        public async Task UpdateAsync(int id, UpdateDocumentDto updateDocumentDto)
        {
            var documentToUpdate = await _kwiatkiBeatkiDbContext.Document.FirstOrDefaultAsync(i => i.Id == id);

            if (documentToUpdate == null)
                throw new NotFoundException("DocumentId", $"Document with ID: {id} was not found");

            documentToUpdate.Remarks = updateDocumentDto.Remarks;

            await _kwiatkiBeatkiDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var documentToDelete = await _kwiatkiBeatkiDbContext.Document.FirstOrDefaultAsync(i => i.Id == id);

            if (documentToDelete == null)
                throw new NotFoundException("DocumentId", $"Document with ID: {id} was not found");

            _kwiatkiBeatkiDbContext.Document.Remove(documentToDelete);
            await _kwiatkiBeatkiDbContext.SaveChangesAsync();
        }
        private async Task<DocumentEntity> PrepareDocumentEntityAsync(CreateDocumentDto createDocumentDto)
        {
            var documentEntity = _mapper.Map<DocumentEntity>(createDocumentDto);
            var lastDocumentNumber = await GetLastDocumentNumberAsync(createDocumentDto.DocumentTypeId);
            var documentTypeAbbreviation = await GetDocumentTypeAbbreviationAsync(createDocumentDto.DocumentTypeId);
            var currentDocumentNumber = lastDocumentNumber + 1;
            var currentFullDocumentNumber = $"{currentDocumentNumber:D2}/{documentTypeAbbreviation}/{documentEntity.Created.ToString("MM")}/{documentEntity.Created.Year}";

            documentEntity.DocumentNumber = currentDocumentNumber;
            documentEntity.FullDocumentNumber = currentFullDocumentNumber;
            documentEntity.UserId = (int)_userContextService.UserId!;

            return documentEntity;
        }

        private async Task<int> GetLastDocumentNumberAsync(int documentTypeId)
        {
            var currentDateTime = DateTime.Now;
            var lastDocumentNumber = await _kwiatkiBeatkiDbContext.Document
                .Where(d => d.DocumentTypeId == documentTypeId && d.Created.Month == currentDateTime.Month && d.Created.Year == currentDateTime.Year)
                .Select(d => d.DocumentNumber)
                .DefaultIfEmpty()
                .MaxAsync();

            return lastDocumentNumber;
        }

        private async Task<string> GetDocumentTypeAbbreviationAsync(int documentTypeId)
        {
            var documentTypeAbbreviation = await _kwiatkiBeatkiDbContext.DocumentType.Where(d => d.Id == documentTypeId).Select(d => d.Abbreviation).FirstAsync();

            return documentTypeAbbreviation;
        }

        public async Task<byte[]> GenerateDocument(int documentId)
        {
            var documentEntity = await _kwiatkiBeatkiDbContext.Document
                .Include(i => i.DocumentType)
                .Include(i => i.WarehouseFrom)
                .Include(i => i.WarehouseTo)
                .Include(i => i.TradePartner)
                .Include(i => i.User)
                    .ThenInclude(i => i.Role)
                .Include(i => i.Lines)
                    .ThenInclude(i => i.Item)
                        .ThenInclude(i => i.MeasurementUnit)
                .Include(i => i.Lines)
                    .ThenInclude(i => i.Item)
                        .ThenInclude(i => i.ItemProperties)
                .FirstOrDefaultAsync(d => d.Id == documentId);

            if (documentEntity is null)
                throw new NotFoundException("DocumentId", $"Document with ID: {documentId} was not found");

            var composeOrder = new ComposeOrder(documentEntity);

            return composeOrder.GeneratePdf();
        }
    }
}
