using FluentValidation;

namespace AviApp.Api.Orders.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Order ID must be greater than 0.");
        
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Order must contain at least one item.");
    }
}