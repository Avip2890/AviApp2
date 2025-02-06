using FluentValidation;

namespace AviApp.Api.Orders.GetOrderById;

public class GetOrderByIdQueryValidator: AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Order ID must be greater than 0.");
    }   
    
}