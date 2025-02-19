using FluentValidation;

namespace AviApp.Api.MenuItem.DeleteMenuItem;

public class DeleteMenuItemCommandValidator : AbstractValidator<DeleteMenuItemCommand>
{
    public DeleteMenuItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Menu item ID must be greater than 0.");
    }
}