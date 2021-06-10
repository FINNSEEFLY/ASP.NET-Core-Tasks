using ASP.NET_Core_MVC__Task_4_5_.Authorization;

namespace ASP.NET_Core_MVC__Task_4_5_.Interfaces
{
    public interface IPermissionsProvider
    {
        public bool IsUserHasPermission(Permission permission);
        public bool IsUserInRole(Role role);
        public bool IsUserHasAnyRole();
    }
}
