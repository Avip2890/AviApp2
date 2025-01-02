using AviApp.Models;
using AviApp.Validators.MenuItemValidators;
using FluentValidation;

namespace AviApp.Validators.OrderValidators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
       
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("CustomerId must be greater than 0.");

       
        RuleFor(x => x.OrderDate)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Order date cannot be in the future.");

        
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Order must contain at least one item.");

       
        RuleForEach(x => x.Items).SetValidator(new MenuItemValidator());
    }
}