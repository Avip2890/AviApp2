using FluentValidation;

namespace AviApp.Api.Customers.GetCustomerById;

public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Customer ID must be greater than 0.");
    }
}