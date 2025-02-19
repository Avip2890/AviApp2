using AviApp.Models;
using FluentValidation;

namespace AviApp.Api.MenuItem.GetAllMenuItems;

public class GetAllMenuItemsQueryValidator : AbstractValidator<MenuItemDto>
{
    public GetAllMenuItemsQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Query request cannot be null.");
        
    }
}