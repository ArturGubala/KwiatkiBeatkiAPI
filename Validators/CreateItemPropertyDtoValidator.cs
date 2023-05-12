using FluentValidation;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.ItemProperty;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.Validators
{
    public class CreateItemPropertyDtoValidator : AbstractValidator<CreateItemPropertyDto>
    {
        public CreateItemPropertyDtoValidator(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IActionContextAccessor actionContextAccessor)
        {

            RuleFor(ip => ip.Value)
                .NotNull()
                .MaximumLength(50);

            RuleFor(ip => ip.PropertyId)
                .NotNull();
            RuleFor(ip => ip.PropertyId)
                .Custom((value, context) =>
                {
                    int? itemId = int.TryParse((string?)actionContextAccessor.ActionContext.RouteData.Values.GetValueOrDefault("itemId"), out int id) ? id : default;
                        
                    var itemPropertyEntity = kwiatkiBeatkiDbContext.ItemProperty
                        .Include(ip => ip.Item)
                        .Include(ip => ip.Property)
                        .FirstOrDefault(ip => ip.ItemId == itemId && ip.PropertyId == value);

                    if (itemPropertyEntity != null)
                        context.AddFailure($"ItemProperty", $"Na kartotece [{itemPropertyEntity.Item.StockCode}] " +
                            $"jest już cecha [{itemPropertyEntity.Property.Name}] " +
                            $"o wartości [{itemPropertyEntity.Value}]");
                });
        }
    }
}
