using FluentValidation;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.Item;

namespace KwiatkiBeatkiAPI.Validators
{
    public class CreateItemDtoValidator : AbstractValidator<CreateItemDto>
    {

        public CreateItemDtoValidator(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext)
        {
            RuleFor(i => i.ItemTypeId)
                .NotEmpty();

            RuleFor(i => i.MeasurementUnitId)
                .NotEmpty();

            RuleFor(i => i.Name)
                .MaximumLength(100);

            RuleFor(i => i.Alias)
                .MaximumLength(50);

            RuleFor(i => i.BarCode)
                .MaximumLength(50);

            RuleFor(i => i.StockCode)
                .NotEmpty()
                .MaximumLength(50)
                .Custom((value, context) =>
                {
                    var stockCodeExists = kwiatkiBeatkiDbContext.Item.Any(i => i.StockCode == value);
                    if (stockCodeExists)
                        context.AddFailure("{PropertyNme}", "Kartoteka '{PropertyValue}' istnieje już w bazie");
                });
        }
    }
}
