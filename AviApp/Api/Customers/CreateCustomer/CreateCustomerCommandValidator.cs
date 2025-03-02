using FluentValidation;

namespace AviApp.Api.Customers;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.CreateCustomerRequest.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(50).WithMessage("Customer name must be at most 50 characters long.");

        RuleFor(x => x.CreateCustomerRequest.Phone)
            .Matches(@"^\d+$").WithMessage("Phone number must contain only digits.")
            .Length(10).WithMessage("Phone number must be exactly 10 digits.");
    }
}