using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using backend.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using backend.Hubs;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly IConfiguration config;
        private readonly UserManager<OutlookUser> userManager;
        private readonly IHubContext<ArticleHub, IArticleHub> hubContext;

        public CommentsController(OutlookContext context, IConfiguration config, UserManager<OutlookUser> userManager, IHubContext<ArticleHub, IArticleHub> articlehub)
        {
            this.context = context;
            this.config = config;
            this.userManager = userManager;
            this.hubContext = articlehub;
        }

        public class PostCommentModel
        {
            public int articleId;
            public string text;
        }

        // GET: api/Comments
        [HttpGet("{articleID}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int articleID)
        {
            var comments = from comment in context.Comment
                           where comment.ArticleID == articleID
                           select comment;

            return await comments.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("Comment/{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await context.Comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            var username = HttpContext.User.FindFirst("name")?.Value;
            var user = await userManager.FindByNameAsync(username);

            if (comment.UserID != user.Id)
            {
                return ValidationProblem("Only the owner of this comment is allowed to edit it.");
            }

            context.Entry(comment).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            var article = await context.Article.FindAsync(comment.ArticleID);

            FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"{DateTime.Now} | {user.UserName} editted his comment `{comment.Text}` on the article of title `{article.Title}`");

            return NoContent();
        }

        // POST: api/Comments
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment _comment)
        {
            var username = HttpContext.User.FindFirst("name")?.Value;
            var user = await userManager.FindByNameAsync(username);

            var comment = new Comment() { ArticleID = _comment.ArticleID, Text = _comment.Text, DateTime = DateTime.Now, UserID = user.Id };

            context.Comment.Add(comment);
            await context.SaveChangesAsync();

            var article = await context.Article.FindAsync(comment.ArticleID);

            var comments = await context.Comment.Where(c => c.ArticleID == article.Id).ToListAsync();

            foreach (var comment_ in comments)
            {
                var owner = await context.Users.FindAsync(comment_.UserID);
                comment_.User = owner;
            }

            await hubContext.Clients.All.ArticleCommentChange(article.Id, comments);

            FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"{DateTime.Now} | {user.UserName} posted a comment `{comment.Text}` on the article of title `{article.Title}`");

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            var article = await context.Article.FindAsync(comment.ArticleID);
            FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"{DateTime.Now} | {HttpContext.User.Identity.Name} attempts to delete his comment `{comment.Text}` on the article of title `{article.Title}`");

            var username = HttpContext.User.FindFirst("name")?.Value;
            var user = await userManager.FindByNameAsync(username);

            if (comment.UserID != user.Id)
            {
                return ValidationProblem("Only the owner of this comment is allowed to delete it.");
            }

            context.Comment.Remove(comment);
            await context.SaveChangesAsync();

            FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"{DateTime.Now} | Delete Completed");

            return comment;
        }

        private bool CommentExists(int id)
        {
            return context.Comment.Any(e => e.Id == id);
        }
    }
}
