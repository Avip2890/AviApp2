using MediatR;
using AviApp.Interfaces;
using AviApp.Results;
using AviApp.Models;

namespace AviApp.Api.UserRole.GetUsersByRole;

    public class GetUsersByRoleHandler(IUserRoleService userRoleService)
        : IRequestHandler<GetUsersByRoleQuery, Result<List<UserDto>>>
    {
        public async Task<Result<List<UserDto>>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await userRoleService.GetUsersByRoleAsync(request.RoleId, cancellationToken);
            return result.IsSuccess ? result.Value : result.Errors;
        }
    }
