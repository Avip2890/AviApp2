using AviApp.Models;
using FluentValidation;

namespace AviApp.Validators.CustomerValidator;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c=>c.CustomerName)
            .NotEmpty().WithMessage("Customer name is required")
            .MaximumLength(50);

        RuleFor(x => x.Phone)
            .Matches(@"^\d+$").WithMessage("Phone number must contain only digits.")
            .Length(10).WithMessage("Phone number must be exactly 10 digits.");
    }

}