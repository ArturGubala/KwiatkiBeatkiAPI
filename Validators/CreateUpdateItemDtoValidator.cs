using FluentValidation;
using KwiatkiBeatkiAPI.Models.Item;

namespace KwiatkiBeatkiAPI.Validators
{
    public class CreateUpdateItemDtoValidator : AbstractValidator<CreateUpdateItemDto>
    {

        public CreateUpdateItemDtoValidator()
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
                .MaximumLength(50);
        }
    }
}
