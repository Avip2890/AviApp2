using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.UserRole.GetRolesForUser;

    public class GetRolesForUserQueryHandler(IUserRoleService userRoleService)
        : IRequestHandler<GetRolesForUserQuery, Result<List<RoleDto>>>
    {
        public async Task<Result<List<RoleDto>>> Handle(GetRolesForUserQuery request, CancellationToken cancellationToken)
        {
            var result = await userRoleService.GetRolesForUserAsync(request.UserId, cancellationToken);
            return result.IsSuccess ? result.Value : result.Errors;
        }
    }
