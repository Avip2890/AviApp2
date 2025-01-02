using AviApp.Models;
using FluentValidation;

namespace AviApp.Validators.MenuItemValidators;

public class MenuItemValidator : AbstractValidator<MenuItem>
{
    public MenuItemValidator()
    {
        // בדיקה ששם הפריט אינו ריק
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Menu item name is required.");

        // בדיקה שתיאור הפריט אינו ריק
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Menu item description is required.");

        // בדיקה שהמחיר גדול מ-0
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        // בדיקה ש-IsAvailable מוגדר לערך תקין
        RuleFor(x => x.IsAvailable)
            .NotNull().WithMessage("IsAvailable must be specified.");
    }
}