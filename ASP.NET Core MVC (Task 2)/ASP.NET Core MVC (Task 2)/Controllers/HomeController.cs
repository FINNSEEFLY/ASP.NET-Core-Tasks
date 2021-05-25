using ASP.NET_Core_MVC__Task_2_.Filters;
using ASP.NET_Core_MVC__Task_2_.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ASP.NET_Core_MVC__Task_2_.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProfilesService _profilesService;

        public HomeController(IProfilesService profilesService)
        {
            _profilesService = profilesService;
        }

        [RateLimitFilter(RateLimit = 2)]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParam = string.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewBag.FirstNameSortParam = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.LastNameSortParam = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.BirthdaySortParam = sortOrder == "Birthday" ? "Birthday_desc" : "Birthday";

            var profiles = await _profilesService.GetProfiles();

            if (profiles == null)
            {
                return new StatusCodeResult(500);
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                profiles = profiles.Where(
                    p => p.Id.ToString().Contains(searchString)
                         || p.FirstName.Contains(searchString)
                         || p.LastName.Contains(searchString)
                         || p.Birthday.ToString("d").Contains(searchString));
            }


            profiles = sortOrder switch
            {
                "Id_desc" => profiles.OrderByDescending(p => p.Id),
                "FirstName_desc" => profiles.OrderByDescending(p => p.FirstName),
                "FirstName" => profiles.OrderBy(p => p.FirstName),
                "LastName" => profiles.OrderBy(p => p.LastName),
                "LastName_desc" => profiles.OrderByDescending(p => p.LastName),
                "Birthday" => profiles.OrderBy(p => p.Birthday),
                "Birthday_desc" => profiles.OrderByDescending(p => p.Birthday),
                _ => profiles.OrderBy(p => p.Id),
            };

            const int pageSize = 5;
            var pageNumber = (page ?? 1);

            return View(profiles.ToPagedList(pageNumber, pageSize));
        }
    }
}