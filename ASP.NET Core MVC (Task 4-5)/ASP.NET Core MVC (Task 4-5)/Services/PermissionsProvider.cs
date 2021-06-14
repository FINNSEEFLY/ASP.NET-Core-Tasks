using ASP.NET_Core_MVC__Task_4_5_.Authorization;
using ASP.NET_Core_MVC__Task_4_5_.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace ASP.NET_Core_MVC__Task_4_5_.Services
{
    public class PermissionsProvider : IPermissionsProvider
    {
        private readonly Permission _permissions;
        private readonly Role _role;

        public PermissionsProvider(RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessorAccessor)
        {
            (_permissions, _role) = IdentifyUser(httpContextAccessorAccessor, roleManager);
        }

        public bool IsUserHasPermission(Permission permission)
        {
            return _permissions.HasFlag(permission);
        }

        public bool IsUserInRole(Role role)
        {
            return role == _role;
        }

        public bool IsUserHasAnyRole()
        {
            return _role != Role.None;
        }

        private static (Permission, Role) IdentifyUser(IHttpContextAccessor httpContextAccessor,
            RoleManager<IdentityRole> roleManager)
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                return ((Permission)Role.None, Role.None);
            }

            var role = roleManager.Roles.Select(r=>r.Name)
                                               .AsEnumerable()
                                               .FirstOrDefault(roleName => user.IsInRole(roleName));

            if (role == null)
            {
                return ((Permission)Role.None, Role.None);
            }

            var permissions = RoleHelper.GetPermissionsByRoleName(role);

            return (permissions, Enum.Parse<Role>(role));
        }
    }
}