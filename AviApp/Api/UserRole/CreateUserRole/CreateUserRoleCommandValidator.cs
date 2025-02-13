using AviApp.Interfaces;
using FluentValidation;

namespace AviApp.Api.UserRole.CreateUserRole;

public class CreateUserRoleCommandValidator : AbstractValidator<CreateUserRoleCommand>
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    public CreateUserRoleCommandValidator(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
        
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0.");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId must be greater than 0.");

        RuleFor(x => x)
            .MustAsync(ExistValidUserRole).WithMessage("Invalid UserId or RoleId.");
    }

    private async Task<bool> ExistValidUserRole(CreateUserRoleCommand command, CancellationToken cancellationToken)
    {
        // בדוק אם המשתמש קיים
        var userExists = await _userService.UserExistsAsync(command.UserId, cancellationToken);
        if (!userExists) return false;

        // בדוק אם התפקיד קיים
        var roleExists = await _roleService.RoleExistsAsync(command.RoleId, cancellationToken);
        if (!roleExists) return false;

        return true;
    }
}