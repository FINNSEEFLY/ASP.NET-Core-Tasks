using ASP.NET_Core_MVC__Task_2_.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC__Task_2_.Interfaces
{
    public interface IProfilesService
    {
        public Task<IEnumerable<Profile>> GetProfiles();
    }
}
