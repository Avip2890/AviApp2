using AviApp.Models;
using FluentValidation;

namespace AviApp.Validators.MenuItemValidators;

public class MenuItemValidator : AbstractValidator<MenuItemDto>
{
    public MenuItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.IsAvailable)
            .NotNull().WithMessage("Availability status must be specified.");
    }
}