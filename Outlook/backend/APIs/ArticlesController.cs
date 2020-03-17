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
        [HttpGet("{issueID}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles(int issueID)
        {
            var articles = from article in context.Article
                           where article.IssueID == issueID
                           select article;

            foreach (var article in articles)
            {
                await GetArticleProperties(article);
            }

            return await articles.ToListAsync();
        }

        // GET: api/Articles/Article/5
        [HttpGet("Article/{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await context.Article.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            await GetArticleProperties(article);

            return article;
        }

        private async Task GetArticleProperties(Article article)
        {
            // Add the category name
            var category = await context.Category.FindAsync(article.CategoryID);
            article.Category = category.CategoryName;

            // Add the writer name
            var writer = await context.Member.FindAsync(article.MemberID);
            article.Writer = writer.Name;

            // Add the comment list on the article
            var comments = from comment in context.Comment
                           where comment.ArticleID == article.Id
                           select comment;

            // Add replies list for each comment
            foreach (var comment in comments)
            {
                var replies = from reply in context.Reply
                              where reply.CommentID == comment.Id
                              select reply;

                comment.Replies = await replies.ToListAsync();
            }

            article.Comments = await comments.ToListAsync();
        }
    }
}
