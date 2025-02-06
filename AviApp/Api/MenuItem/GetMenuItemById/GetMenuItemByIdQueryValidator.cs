using AviApp.Api.MenuItem.MenuItemQueries;
using FluentValidation;

namespace AviApp.Api.MenuItem.GetMenuItemById;

public class GetMenuItemByIdQueryValidator : AbstractValidator<GetMenuItemByIdQuery>
{
    public GetMenuItemByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Menu item ID must be greater than 0.");
    }
}