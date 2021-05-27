using ASP.NET_Core_Web_API__Task_3_.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NET_Core_Web_API__Task_3_.Interfaces
{
    public interface IArticleService
    {
        public Task<IEnumerable<ArticleModel>> GetArticles();
        public Task<IEnumerable<ArticleModel>> GetArticles(int authorId);
        public Task<ArticleModel> GetArticle(int articleId);
    }
}
