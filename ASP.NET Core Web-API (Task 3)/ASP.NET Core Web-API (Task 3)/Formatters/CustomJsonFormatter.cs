using ASP.NET_Core_Web_API__Task_3_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASP.NET_Core_Web_API__Task_3_.Formatters
{
    public class CustomJsonFormatter : TextOutputFormatter
    {
        public CustomJsonFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json+custom"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            return typeof(ArticleModel).IsAssignableFrom(type) ||
                   typeof(IEnumerable<ArticleModel>).IsAssignableFrom(type);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var services = httpContext.RequestServices;

            var linkGenerator = services.GetRequiredService<LinkGenerator>();
            var stream = new MemoryStream();

            if (context.Object is IEnumerable<ArticleModel> articles)
            {
                foreach (var article in articles)
                {
                    await FormatArticle(stream, article, linkGenerator, httpContext);
                }
            }
            else
            {
                await FormatArticle(stream, (ArticleModel) context.Object, linkGenerator, httpContext);
            }

            await httpContext.Response.BodyWriter.AsStream().WriteAsync(stream.ToArray());
        }

        private static async Task FormatArticle(Stream stream, ArticleModel article, LinkGenerator generator,
            HttpContext context)
        {
            await JsonSerializer.SerializeAsync(
                stream,
                new
                {
                    data = article,
                    _links = new Dictionary<string, object>
                    {
                        ["self"] = generator.GetUriByAction(
                            context,
                            "GetArticleById",
                            "Article",
                            new
                            {
                                id = article.Id,
                                area = "api"
                            }),
                        ["get-author"] = generator.GetUriByAction(
                            context,
                            "GetArticlesByProfileId",
                            "Profile",
                            new
                            {
                                id = article.AuthorId,
                                area = "api"
                            })
                    }
                },
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }
    }
}