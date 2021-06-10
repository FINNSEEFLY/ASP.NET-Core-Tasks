using ASP.NET_Core_Web_API__Task_3_.Interfaces;
using ASP.NET_Core_Web_API__Task_3_.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASP.NET_Core_Web_API__Task_3_.Services
{
    public class ArticleService : IArticleService
    {
        private readonly string _articlePath;
        private readonly ILogger _logger;

        public ArticleService(IConfiguration configuration, ILogger<ArticleService> logger)
        {
            _articlePath = configuration.GetValue<string>("ArticleJsonPath");
            _logger = logger;
        }

        public async Task<IEnumerable<ArticleModel>> GetArticles()
        {
            return await LoadArticles();
        }

        public async Task<IEnumerable<ArticleModel>> GetArticles(int authorId)
        {
            return (await LoadArticles())?.Where(a => a.AuthorId == authorId);
        }

        public async Task<ArticleModel> GetArticle(int articleId)
        {
            return (await LoadArticles())?.FirstOrDefault(a => a.Id == articleId);
        }

        private async Task<IEnumerable<ArticleModel>> LoadArticles()
        {
            IEnumerable<ArticleModel> articles = null;

            try
            {
                await using var fileStream = File.OpenRead(_articlePath);
                articles = await JsonSerializer.DeserializeAsync<IEnumerable<ArticleModel>>(fileStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Can't load articles");
            }

            return articles;
        }
    }
}