using FluentValidation;

namespace AviApp.Api.MenuItem.GetAllMenuItems;

public class GetAllMenuItemsQueryValidator : AbstractValidator<GetAllMenuItemsQuery>
{
    public GetAllMenuItemsQueryValidator()
    {
        RuleFor(_ => true) 
            .Must(_ => true)
            .WithMessage("Invalid query.");
    }
}