using FluentValidation;

namespace AviApp.Api.Customers.DeleteCustomer;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Customer ID must be greater than 0.");
    }
}