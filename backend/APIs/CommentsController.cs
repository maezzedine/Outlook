using backend.Data;
using backend.Hubs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly IdentityService identityService;
        private readonly IHubContext<ArticleHub, IArticleHub> articleHub;
        private readonly Logger.Logger logger;

        public CommentsController(
            OutlookContext context, 
            IdentityService identityService,
            IHubContext<ArticleHub, IArticleHub> articleHub)
        {
            this.context = context;
            this.identityService = identityService;
            this.articleHub = articleHub;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.web);
        }

        public class PostCommentModel
        {
            public int articleId;
            public string text;
        }

        // POST: api/Comments
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment _comment)
        {
            var user = await identityService.GetUserWithToken(HttpContext);

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

            await articleHub.Clients.All.ArticleCommentChange(article.Id, comments);

            logger.Log($"{user.UserName} posted a comment `{comment.Text}` on the article of title `{article.Title}`");

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

            var user = await identityService.GetUserWithToken(HttpContext);
            var article = await context.Article.FindAsync(comment.ArticleID);

            if (comment.UserID != user.Id)
            {
                return ValidationProblem("Only the owner of this comment is allowed to delete it.");
            }

            logger.Log($"{user.UserName} attempts to delete his comment `{comment.Text}` on the article of title `{article.Title}`");

            context.Comment.Remove(comment);
            await context.SaveChangesAsync();

            logger.Log("Delete Completed");

            var comments = await context.Comment.Where(c => c.ArticleID == article.Id).ToListAsync();
            await articleHub.Clients.All.ArticleCommentChange(article.Id, comments);

            return Ok();
        }
    }
}
