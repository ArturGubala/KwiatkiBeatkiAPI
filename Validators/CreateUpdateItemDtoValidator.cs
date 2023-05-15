using FluentValidation;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.Item;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace KwiatkiBeatkiAPI.Validators
{
    public class CreateUpdateItemDtoValidator : AbstractValidator<CreateUpdateItemDto>
    {

        public CreateUpdateItemDtoValidator(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IActionContextAccessor actionContextAccessor)
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
                    var httpMethod = actionContextAccessor.ActionContext.HttpContext.Request.Method;
                    bool? stockCodeExists = default;

                    if (httpMethod.ToUpper() == "PUT")
                    {
                        int? itemIdFromRoute = int.TryParse((string?)actionContextAccessor.ActionContext.RouteData.Values.GetValueOrDefault("id"), out int id) ? id : default;
                        stockCodeExists = kwiatkiBeatkiDbContext.Item.Any(i => i.StockCode == value && i.Id != itemIdFromRoute);
                    }
                    else if (httpMethod.ToUpper() == "POST")
                        stockCodeExists = kwiatkiBeatkiDbContext.Item.Any(i => i.StockCode == value);

                    if (stockCodeExists != null && stockCodeExists == true)
                        context.AddFailure("StockCode", $"Kartoteka [{value}] istnieje już w bazie");
                });
        }
    }
}
