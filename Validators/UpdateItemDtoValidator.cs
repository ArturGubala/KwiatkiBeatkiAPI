using FluentValidation;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.Item;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace KwiatkiBeatkiAPI.Validators
{
    public class UpdateItemDtoValidator : AbstractValidator<UpdateItemDto>
    {

        public UpdateItemDtoValidator(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IActionContextAccessor actionContextAccessor)
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
                    int? itemIdFromRoute = int.TryParse((string?)actionContextAccessor.ActionContext.RouteData.Values.GetValueOrDefault("id"), out int id) ? id : default;

                    var stockCodeExists = kwiatkiBeatkiDbContext.Item.Any(i => i.StockCode == value && i.Id != itemIdFromRoute);
                    if (stockCodeExists)
                        context.AddFailure($"StockCode", $"Kartoteka [{value}] istnieje już w bazie");
                });
        }
    }
}
