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
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET: api/<ArticleController>
        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleService.GetArticles();

            return articles == null
                ? new StatusCodeResult(StatusCodes.Status500InternalServerError)
                : Ok(articles.ToArray());
        }

        // GET api/<ArticleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            var article = await _articleService.GetArticle(id);

            return article == null
                ? new StatusCodeResult(StatusCodes.Status404NotFound)
                : Ok(article);
        }
    }
}