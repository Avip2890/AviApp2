using AviApp.Models;
using FluentValidation;

namespace AviApp.Api.Order.OrderValidators;

public abstract class OrderValidator : AbstractValidator<OrderDto>
{
    protected OrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("CustomerId must be greater than 0.");

        RuleFor(x => x.OrderDate)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Order date cannot be in the future.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Order must contain at least one item.");

        RuleForEach(x => x.Items)
            .GreaterThan(0).WithMessage("Each item ID must be greater than 0.");
    }
}