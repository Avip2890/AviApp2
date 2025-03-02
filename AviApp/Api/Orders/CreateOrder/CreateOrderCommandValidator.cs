using FluentValidation;

namespace AviApp.Api.Orders.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CreateOrderRequest.CustomerId)
            .GreaterThan(0).WithMessage("Customer ID must be greater than 0.");

        RuleFor(x => x.CreateOrderRequest.OrderMenuItems)
            .NotEmpty().WithMessage("Orders must contain at least one item.");
        
        RuleFor(x => x.CreateOrderRequest.OrderDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Orders date cannot be in the future.");
    }
}
