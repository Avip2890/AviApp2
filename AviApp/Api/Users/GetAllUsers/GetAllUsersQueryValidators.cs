using FluentValidation;
using AviApp.Domain.Entities;

namespace AviApp.Api.Users.GetAllUsers
{
    public class GetAllUsersQueryValidators : AbstractValidator<GetAllUsersQuery>
    {
        public GetAllUsersQueryValidators()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("Query request cannot be null.");
        }
    }
}