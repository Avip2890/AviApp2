
using FluentValidation;

namespace AviApp.Api.Customers.GetAllCustomers;

public class GetAllCustomerQueryValidator : AbstractValidator<GetAllCustomersQuery>
{
    public GetAllCustomerQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Query request cannot be null.");
    }
}

