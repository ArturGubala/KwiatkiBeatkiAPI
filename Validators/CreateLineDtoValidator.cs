using FluentValidation;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.Line;

namespace KwiatkiBeatkiAPI.Validators
{
    public class CreateLineDtoValidator : AbstractValidator<CreateLineDto>
    {
        public CreateLineDtoValidator(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext)
        {
            RuleFor(d => d.ItemId)
                .NotEmpty()
                .NotNull()
                .Custom((value, context) =>
                {
                    var itemExists = kwiatkiBeatkiDbContext.Item.Any(i => i.Id == value);
                    if (!itemExists)
                        context.AddFailure("Nie znaleziono kartoteki w bazie");
                });
        }
    }
}
