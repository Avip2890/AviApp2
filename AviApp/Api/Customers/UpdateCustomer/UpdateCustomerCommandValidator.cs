using AviApp.Api.Customers.UpdateCustomer;
using FluentValidation;

namespace AviApp.Api.Customers.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Customer ID must be greater than 0.");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(50).WithMessage("Customer name must be at most 50 characters long.");

        RuleFor(x => x.Phone)
            .Matches(@"^\d+$").WithMessage("Phone number must contain only digits.")
            .Length(10).WithMessage("Phone number must be exactly 10 digits.");
    }
}