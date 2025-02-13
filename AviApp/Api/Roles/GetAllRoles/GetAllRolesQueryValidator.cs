using AviApp.Api.Orders.GetAllOrders;
using AviApp.Models;
using FluentValidation;

namespace AviApp.Api.Roles.GetAllRoles;

public class GetAllRolesQueryValidator : AbstractValidator<GetAllRolesQuery>
{
    public GetAllRolesQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Query request cannot be null.");
    }
    
}