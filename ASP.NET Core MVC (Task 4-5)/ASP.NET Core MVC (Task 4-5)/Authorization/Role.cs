using System;

namespace ASP.NET_Core_MVC__Task_4_5_.Authorization
{
    public enum Role
    {
        User = Permission.GetProfiles | Permission.GetProfileById | Permission.AddProfile,

        Manager = Permission.GetProfiles | Permission.GetProfileById | Permission.AddProfile | Permission.UpdateProfile,

        Admin = Permission.GetProfiles | Permission.GetProfileById | Permission.AddProfile | Permission.UpdateProfile |
                Permission.DeleteProfile,

        None = Permission.None
    }

    public static class RoleHelper
    {
        public static Permission GetPermissionsByRoleName(string roleName)
        {
            if (!Enum.TryParse<Role>(roleName, out var role))
            {
                return (Permission)Role.None;
            }

            return (Permission)role;
        }
    }
}