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

            CreateMap<DocumentEntity, DocumentDto>()
                .ForMember(m => m.DocumentType, c => c.MapFrom(s => s.DocumentType.Name))
                .ForMember(m => m.User, c => c.MapFrom(s => s.User))
                .ForMember(m => m.WarehouseFromName, c => c.MapFrom(s => s.WarehouseFrom.Name))
                .ForMember(m => m.WarehouseToName, c => c.MapFrom(s => s.WarehouseTo.Name))
                .ForMember(m => m.TradePartnerName, c => c.MapFrom(s => s.TradePartner.Name))
                .ForMember(m => m.Lines, c => c.MapFrom(s => s.Lines));

            CreateMap<CreateLineDto, LineEntity>();
            CreateMap<LineEntity, LineDto>()
                .ForMember(m => m.StockCode, c => c.MapFrom(s => s.Item.StockCode))
                .ForMember(m => m.Alias, c => c.MapFrom(s => s.Item.Alias))
                .ForMember(m => m.MeasurementUnitAbbrev, c => c.MapFrom(s => s.Item.MeasurementUnit.Abbreviation));
        }
    }
}
