/*using AviApp.Models;
using FluentValidation;

namespace AviApp.Api.Customers.CustomerValidator;

public abstract class CustomerValidator : AbstractValidator<CustomerDto>
{
    protected CustomerValidator()
    {
        RuleFor(c=>c.CustomerName)
            .NotEmpty().WithMessage("Customers name is required")
            .MaximumLength(50);

        RuleFor(x => x.Phone)
            .Matches(@"^\d+$").WithMessage("Phone number must contain only digits.")
            .Length(10).WithMessage("Phone number must be exactly 10 digits.");
    }

}*/