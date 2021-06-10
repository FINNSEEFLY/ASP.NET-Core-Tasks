using System;

namespace ASP.NET_Core_MVC__Task_4_5_.Authorization
{
    [Flags]
    public enum Permission
    {
        None = 0,
        GetProfileById = 1,
        GetProfiles = 1 << 1,
        AddProfile = 1 << 2,
        UpdateProfile = 1 << 3,
        DeleteProfile = 1 << 4,
    }

}

