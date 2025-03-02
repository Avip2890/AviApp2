using FluentValidation;

namespace AviApp.Api.MenuItem.CreateMenuItem;

public class CreateMenuItemCommandValidator : AbstractValidator<CreateMenuItemCommand>
{
    public CreateMenuItemCommandValidator()
    {
        RuleFor(x => x.CreateMenuItemRequest.Name)
            .NotEmpty().WithMessage("Menu item name is required.")
            .MaximumLength(100).WithMessage("Menu item name must be at most 100 characters long.");

        RuleFor(x => x.CreateMenuItemRequest.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.CreateMenuItemRequest.Description)
            .MaximumLength(500).WithMessage("Description must be at most 500 characters long.");

        RuleFor(x => x.CreateMenuItemRequest.IsAvailable)
            .NotNull().WithMessage("Availability status is required.");

     
    }
}