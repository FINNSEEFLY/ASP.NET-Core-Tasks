using ASP.NET_Core_MVC__Task_2_.Interfaces;
using ASP.NET_Core_MVC__Task_2_.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC__Task_2_.Services
{
    public class ProfilesService : IProfilesService
    {
        private const string ProfilePath = "json-file.json";

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            IEnumerable<Profile> profiles = null;
            try
            {
                await using var fileStream = File.OpenRead(ProfilePath);
                profiles = await JsonSerializer.DeserializeAsync<IEnumerable<Profile>>(fileStream);
            }
            catch
            {
                // ignored
            }

            // Thread.Sleep(15000);
            return profiles;
        }
    }
}