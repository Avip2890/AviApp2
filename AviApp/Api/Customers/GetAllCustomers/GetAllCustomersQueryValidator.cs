using FluentValidation;

namespace AviApp.Api.Customers.GetAllCustomers;

public class GetAllCustomersQueryValidator : AbstractValidator<GetAllCustomersQuery>
{
    public GetAllCustomersQueryValidator()
    {
        RuleFor(_ => true)
            .Must(_ => true)
            .WithMessage("Invalid query");
    }
}