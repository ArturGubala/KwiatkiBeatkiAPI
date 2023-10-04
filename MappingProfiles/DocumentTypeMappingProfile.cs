using AutoMapper;
using KwiatkiBeatkiAPI.Entities.DocumentType;
using KwiatkiBeatkiAPI.Models.DocumentType;

namespace KwiatkiBeatkiAPI.MappingProfiles
{
    public class DocumentTypeMappingProfile : Profile
    {
        public DocumentTypeMappingProfile()
        {
            CreateMap<DocumentTypeEntity, DocumentTypeDto>();
        }
    }
}
