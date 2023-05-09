using FluentValidation;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.Document;

namespace KwiatkiBeatkiAPI.Validators
{
    public class CreateDocumentDtoValidator : AbstractValidator<CreateDocumentDto>
    {
        public CreateDocumentDtoValidator(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext)
        {
            RuleFor(d => d.DocumentTypeId)
                .NotEmpty()
                .NotNull()
                .Custom((value, context) =>
                {
                    var documentTypeExists = kwiatkiBeatkiDbContext.DocumentType.Any(d => d.Id == value);
                    if (!documentTypeExists)
                        context.AddFailure("Podany rodzaj dokumentu nie istnieje");
                });

            RuleFor(d => d.TradePartnerId)
                .Custom((value, context) =>
                {
                    if (value > 0 || value is not null)
                    {
                        var tradePartnerExists = kwiatkiBeatkiDbContext.TradePartner.Any(d => d.Id == value);
                        if (!tradePartnerExists)
                            context.AddFailure("Nie znaleziono kontrahenta w bazie");
                    }
                });

            RuleFor(d => d.WarehouseFromId)
                .Custom((value, context) =>
                {
                    if (value > 0 && value != null)
                    {
                        var warehouseExists = kwiatkiBeatkiDbContext.Warehouse.Any(d => d.Id == value);
                        if (!warehouseExists)
                            context.AddFailure("Nie znaleziono magazynu w bazie");
                    }
                });

            RuleFor(d => d.WarehouseToId)
                .Custom((value, context) =>
                {
                    if (value > 0 && value != null)
                    {
                        var warehouseExists = kwiatkiBeatkiDbContext.Warehouse.Any(d => d.Id == value);
                        if (!warehouseExists)
                            context.AddFailure("Nie znaleziono magazynu w bazie");
                    }
                });
        }
    }
}
