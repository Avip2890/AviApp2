using FluentValidation;

namespace AviApp.Api.UserRole.DeleteUserRole;

public class DeleteUserRoleCommandValidator : AbstractValidator< DeleteUserRoleCommand>
{
    public DeleteUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.RoleId).GreaterThan(0);
    }
    
}