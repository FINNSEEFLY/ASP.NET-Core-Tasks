using ASP.NET_Core_Web_API__Task_3_.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.NET_Core_Web_API__Task_3_.Controllers
{
    [ApiController]
    [Area("api")]
    [Route("[area]/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ProfileController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET api/<ProfileController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticlesByProfileId(int id)
        {
            var articles = await _articleService.GetArticles(id);

            if (articles == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            var articleModels = articles.ToArray();

            if (!articleModels.Any())
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            return Ok(articleModels);
        }
    }
}