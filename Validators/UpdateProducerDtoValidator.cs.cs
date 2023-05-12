using FluentValidation;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.Producer;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace KwiatkiBeatkiAPI.Validators
{
    public class UpdateProducerDtoValidator : AbstractValidator<UpdateProducerDto>
    {

        public UpdateProducerDtoValidator(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IActionContextAccessor actionContextAccessor)
        {
            RuleFor(p => p.Name)
                .NotNull()
                .MaximumLength(50)
                .Custom((value, context) =>
                {
                    int? producerIdFromRoute = int.TryParse((string?)actionContextAccessor.ActionContext.RouteData.Values.GetValueOrDefault("id"), out int id) ? id : default;

                    var producerExists = kwiatkiBeatkiDbContext.Producer.Any(p => p.Name == value && p.Id != producerIdFromRoute);
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
