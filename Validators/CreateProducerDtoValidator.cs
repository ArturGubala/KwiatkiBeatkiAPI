using FluentValidation;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.Producer;

namespace KwiatkiBeatkiAPI.Validators
{
    public class CreateProducerDtoValidator : AbstractValidator<CreateProducerDto>
    {
        public CreateProducerDtoValidator(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext)
        {
            RuleFor(p => p.Name)
                .NotNull()
                .MaximumLength(50)
                .Custom((value, context) =>
                {
                    var producerExists = kwiatkiBeatkiDbContext.Producer.Any(i => i.Name == value);
                    if (producerExists)
                        context.AddFailure("Name", $"Producent o nazwie [{value}] istnieje już w bazie.");
                });

            RuleFor(p => p.PhoneNumber)
                .MaximumLength(50);

            RuleFor(p => p.Email)
                .EmailAddress()
                .MaximumLength(50);

            RuleFor(p => p.Website)
                .MaximumLength(50);
        }
    }
}
