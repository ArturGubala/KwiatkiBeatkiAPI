using AutoMapper;
using KwiatkiBeatkiAPI.Entities.Document;
using KwiatkiBeatkiAPI.Entities.Line;
using KwiatkiBeatkiAPI.Models.Document;
using KwiatkiBeatkiAPI.Models.Line;

namespace KwiatkiBeatkiAPI.MappingProfiles
{
    public class DocumentMappingProfile : Profile
    {
        public DocumentMappingProfile()
        {
            CreateMap<CreateDocumentDto, DocumentEntity>()
                .ForMember(m => m.Lines, y => y.Ignore());

            CreateMap<CreateLineDto, LineEntity>();
        }
    }
}
