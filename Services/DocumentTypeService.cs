using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.DocumentType;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IDocumentTypeService
    {
        Task<IEnumerable<DocumentTypeDto>> GetAsync();
    }

    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IMapper _mapper;

        public DocumentTypeService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IMapper mapper)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentTypeDto>> GetAsync()
        {
                var documentTypeEntities = await _kwiatkiBeatkiDbContext.DocumentType.ToListAsync();
                var documentTypeDtos = _mapper.Map<IEnumerable<DocumentTypeDto>>(documentTypeEntities);

                return documentTypeDtos;
        }
    }
}
