using FluentValidation;

namespace AviApp.Api.Roles.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        {
            
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Role name is required.")  
                .MaximumLength(50).WithMessage("Role name must be at most 50 characters long.") 
                .MinimumLength(3).WithMessage("Role name must be at least 3 characters long.");  
        }
    }
    
}