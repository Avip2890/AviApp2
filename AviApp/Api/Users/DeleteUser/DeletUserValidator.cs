using FluentValidation;

namespace AviApp.Api.Users.DeleteUser;

public class DeleteUserValidator: AbstractValidator <DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("User ID must be greater than 0.");
        
    }
}