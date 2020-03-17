using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly OutlookContext context;

        public ArticlesController(OutlookContext context)
        {
            this.context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticle()
        {
            var articles = await context.Article.ToListAsync();

            foreach (var article in articles)
            {
                GetArticleProperties(article);
            }

            return articles;
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await context.Article.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            GetArticleProperties(article);

            return article;
        }

        private async void GetArticleProperties(Article article)
        {
            // Add the category name
            var category = await context.Category.FindAsync(article.CategoryID);
            article.Category = category.CategoryName;

            // Add the writer name
            var writer = await context.Member.FindAsync(article.MemberID);
            article.Writer = writer.Name;
        }
    }
}
