using FluentValidation;

namespace AviApp.Api.UserRole.GetRolesForUser;

    public class GetRolesForUserQueryValidator : AbstractValidator<GetRolesForUserQuery>
    {
        public GetRolesForUserQueryValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");
        }
    }
