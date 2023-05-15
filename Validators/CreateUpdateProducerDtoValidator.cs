using FluentValidation;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.Producer;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace KwiatkiBeatkiAPI.Validators
{
    public class CreateUpdateProducerDtoValidator : AbstractValidator<CreateUpdateProducerDto>
    {
        public CreateUpdateProducerDtoValidator(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IActionContextAccessor actionContextAccessor)
        {

            RuleFor(p => p.PhoneNumber)
                .MaximumLength(50);

            RuleFor(p => p.Email)
                .EmailAddress()
                .MaximumLength(50);

            RuleFor(p => p.Website)
                .MaximumLength(50);

            RuleFor(p => p.Name)
                .NotNull()
                .MaximumLength(50)
                .Custom((value, context) =>
                {
                    var httpMethod = actionContextAccessor.ActionContext.HttpContext.Request.Method;
                    bool? producerExists = default;

                    if (httpMethod.ToUpper() == "PUT")
                    {
                        int? producerIdFromRoute = int.TryParse((string?)actionContextAccessor.ActionContext.RouteData.Values.GetValueOrDefault("id"), out int id) ? id : default;
                        producerExists = kwiatkiBeatkiDbContext.Producer.Any(p => p.Name == value && p.Id != producerIdFromRoute);
                    }
                    else if (httpMethod.ToUpper() == "POST")
                        producerExists = kwiatkiBeatkiDbContext.Producer.Any(i => i.Name == value);

                    if (producerExists != null && producerExists == true)
                        context.AddFailure("Name", $"Producent o nazwie [{value}] istnieje już w bazie.");
                });
        }
    }
}
