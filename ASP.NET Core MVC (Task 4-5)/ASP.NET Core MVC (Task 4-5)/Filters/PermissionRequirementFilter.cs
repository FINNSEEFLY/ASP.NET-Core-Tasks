using ASP.NET_Core_MVC__Task_4_5_.Authorization;
using ASP.NET_Core_MVC__Task_4_5_.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NET_Core_MVC__Task_4_5_.Filters
{
    public class PermissionRequirementFilter : IAuthorizationFilter
    {
        private readonly Permission _permission;
        private readonly IPermissionsProvider _permissionsProvider;

        public PermissionRequirementFilter(Permission permission, IPermissionsProvider permissionsProvider)
        {
            _permission = permission;
            _permissionsProvider = permissionsProvider;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_permissionsProvider.IsUserHasAnyRole())
            {
                context.Result = new UnauthorizedResult();

                return;
            }

            if (!_permissionsProvider.IsUserHasPermission(_permission))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}