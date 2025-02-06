using FluentValidation;

namespace AviApp.Api.MenuItem.CreateMenuItem;

public class CreateMenuItemCommandValidator : AbstractValidator<CreateMenuItemCommand>
{
    public CreateMenuItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Menu item name is required.")
            .MaximumLength(100).WithMessage("Menu item name must be at most 100 characters long.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must be at most 500 characters long.");

        RuleFor(x => x.IsAvailable)
            .NotNull().WithMessage("Availability status is required.");

     
    }
}