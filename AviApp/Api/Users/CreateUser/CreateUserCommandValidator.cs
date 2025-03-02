using FluentValidation;

namespace AviApp.Api.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x=>x.CreateUserRequest)
            .NotNull().WithMessage("CreateUserRequest is required.");
        
        RuleFor(x => x.CreateUserRequest.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be at most 50 characters long.");
        
        RuleFor(x => x.CreateUserRequest.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        
        RuleFor(x => x.CreateUserRequest.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
    
}