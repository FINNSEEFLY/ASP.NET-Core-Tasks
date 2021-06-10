using ASP.NET_Core_MVC__Task_4_5_.Authorization;
using ASP.NET_Core_MVC__Task_4_5_.Data;
using ASP.NET_Core_MVC__Task_4_5_.Filters;
using ASP.NET_Core_MVC__Task_4_5_.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC__Task_4_5_.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ProfileController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HasPermission(Permission.GetProfileById)]
        public IActionResult GetProfileById(Guid id)
        {
            var profile = _dbContext.Profile.FirstOrDefault(p => p.ProfileId == id);

            if (profile == null)
            {
                return NotFound();
            }

            return View("DetailProfile", profile);
        }

        [HasPermission(Permission.GetProfiles)]
        public IActionResult GetProfiles()
        {
            return View("Profiles", _dbContext.Profile.ToList());
        }

        [HttpGet]
        [HasPermission(Permission.AddProfile)]
        public IActionResult AddProfile()
        {
            return View("CreateProfile");
        }

        [HttpPost]
        [HasPermission(Permission.AddProfile)]
        public async Task<IActionResult> AddProfile(Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            profile.ProfileId = Guid.NewGuid();

            await _dbContext.AddAsync(profile);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("GetProfiles");
        }

        [HttpGet]
        [HasPermission(Permission.UpdateProfile)]
        public IActionResult UpdateProfile(Guid id)
        {
            var profile = _dbContext.Profile.FirstOrDefault(p => p.ProfileId == id);

            if (profile == null)
            {
                return NotFound();
            }

            return View("UpdateProfile", profile);
        }

        [HttpPost]
        [HasPermission(Permission.UpdateProfile)]
        public async Task<IActionResult> UpdateProfile(Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dbContext.Profile.Update(profile);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("GetProfileById", new { id = profile.ProfileId });
        }

        [HasPermission(Permission.DeleteProfile)]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            var profile = _dbContext.Profile.FirstOrDefault(p => p.ProfileId == id);

            if (profile == null)
            {
                return BadRequest();
            }

            _dbContext.Profile.Remove(profile);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("GetProfiles");
        }
    }
}