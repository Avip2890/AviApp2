using FluentValidation;

namespace AviApp.Api.Orders.GetAllOrders;

public class GetAllOrdersQueryValidator : AbstractValidator<GetAllOrdersQuery>
{
    public GetAllOrdersQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Query request cannot be null.");
    }
}