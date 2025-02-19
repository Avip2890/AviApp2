using FluentValidation;

namespace AviApp.Api.Orders.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Customer ID must be greater than 0.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Orders must contain at least one item.")
            .Must(items => items.All(id => id > 0)).WithMessage("All item IDs must be greater than 0.");

        RuleFor(x => x.OrderDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Orders date cannot be in the future.");
    }
}
