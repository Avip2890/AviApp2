using FluentValidation;
using AviApp.Domain.Entities;

namespace AviApp.Api.Users.GetAllUsers
{
    public class GetAllUsersValidators : AbstractValidator<User>
    {
        public GetAllUsersValidators()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(100).WithMessage("UserName cannot exceed 100 characters.");
            
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(256).WithMessage("Password cannot exceed 256 characters.");
            
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");
            
            RuleFor(user => user.CreatedAt)
                .NotNull().WithMessage("CreatedAt is required.");
        }
    }
}