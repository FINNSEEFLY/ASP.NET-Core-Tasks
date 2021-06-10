using ASP.NET_Core_MVC__Task_4_5_.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_MVC__Task_4_5_.Filters
{
    public class HasPermissionAttribute : TypeFilterAttribute
    {
        public HasPermissionAttribute(Permission permission) : base(typeof(PermissionRequirementFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}