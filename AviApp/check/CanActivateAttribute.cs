/*
using System;
using System.Collections.Generic;
using System.Linq;
using Education.Authorization.Permissions;
using Education.Common;
using Education.Enums;
using Education.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Education.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CanActivateAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Dictionary<UserPermissions, PermissionLevel> _permissionLevels;
        private readonly bool _isAnyPermission;

        public CanActivateAttribute(params string[] permissions)
        {
            _permissionLevels = GetPermissionLevels(permissions);
            _isAnyPermission = permissions.Contains(Perm.AnyPermission);
        }

        private Dictionary<UserPermissions, PermissionLevel> GetPermissionLevels(params string[] permissions)
        {
            var permissionLevels = new Dictionary<UserPermissions, PermissionLevel>();

            foreach (var permission in permissions)
            {
                if(permission == Perm.AnyPermission)
                    continue;
                
                var permissionParts = permission.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (permissionParts.Length == 2)
                {
                    var permissionName = permissionParts[0];
                    var permissionLevel = permissionParts[1];

                    if (Enum.TryParse(permissionName, out UserPermissions userPermission) &&
                        Enum.TryParse(permissionLevel, out PermissionLevel level))
                    {
                        permissionLevels.Add(userPermission, level);
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid permission format: {permission}");
                    }
                }
                else if (permissionParts.Length == 1)
                {
                    var permissionName = permissionParts[0];
                    permissionLevels.Add((UserPermissions)Enum.Parse(typeof(UserPermissions), permissionName), PermissionLevel.ReadOnly);
                }
                else
                {
                    throw new ArgumentException($"Invalid permission format: {permission}");
                }
            }

            return permissionLevels;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var currentUserService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();
            
            var user = currentUserService.User;
            
            if (user is null)
            {
                context.Result = new JsonResult(UserIsNotAuthenticatedProblemDetails()) { StatusCode = 401 };
                return;
            }

            
            var redisCachingService = context.HttpContext.RequestServices.GetRequiredService<IRedisCachingService>();
            
            var logoutUserFromRedis = redisCachingService.Get<string>($"{RedisKeys.LogoutUser}-{user.UserId}");
            
            if (!string.IsNullOrEmpty(logoutUserFromRedis))
            {
                redisCachingService.Remove($"{RedisKeys.LogoutUser}-{user.UserId}");
                
                context.Result = new JsonResult(RefreshTokenRevokedProblemDetails()) { StatusCode = 401 };
                return;
            }
            
            var currentUserPermissions = user.Permission;

            if (_isAnyPermission && currentUserPermissions.Any())
                return;

            foreach (var entry in _permissionLevels)
            {
                var permissionName = entry.Key;
                var permissionLevel = entry.Value;

                var userPermission = currentUserPermissions.FirstOrDefault(x => x.PermissionId == (int)permissionName);

                if (userPermission is not null)
                {
                    if ((int)permissionLevel == 0)
                        return;
                    
                    if ((userPermission.Level?.Id ?? (int)PermissionLevel.ReadOnly) >= (int)permissionLevel)
                        return;
                }
            }
            
            context.Result = new JsonResult(ForbiddenProblemDetails()) { StatusCode = 403 };
        }
        private Models.ProblemDetails RefreshTokenRevokedProblemDetails()
        {
            return new Models.ProblemDetails
            {
                Status = 401,
                Title = "RefreshTokenRevoked",
                ErrorDescription = "בוצע עדכון בפרטי המשתמש, יש להתחבר מחדש",
                ErrorCode = "Authorization.RefreshTokenRevoked",
                ExceptionType =  "Authorization.UnauthorizedException",

            };
        }
        private Models.ProblemDetails UserIsNotAuthenticatedProblemDetails()
        {
            return new Models.ProblemDetails
            {
                Status = 401,
                Title = "UnAuthenticatedUser",
                ErrorDescription = "פג תוקף ההתחברות, יש להתחבר מחדש",
                ErrorCode = "Authorization.Unauthorized",
                ExceptionType =  "Authorization.UnauthorizedException",
            };
        }

        private Models.ProblemDetails ForbiddenProblemDetails()
        {
            return new Models.ProblemDetails
            {
                Status = 403,
                Title = "Forbidden",
                ErrorDescription = "אין לך הרשאה מתאימה לבצע פעולה זו",
                ErrorCode = "Authorization.CanActivate",
                ExceptionType =  "Authorization.ForbiddenException",
            };
        }
    }
}
*/
