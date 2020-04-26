using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using backend.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using backend.Hubs;
using backend.Services;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly UserManager<OutlookUser> userManager;
        private readonly IHubContext<ArticleHub, IArticleHub> hubContext;
        private readonly Logger.Logger logger;

        public CommentsController(OutlookContext context, UserManager<OutlookUser> userManager, IHubContext<ArticleHub, IArticleHub> articlehub)
        {
            this.context = context;
            this.userManager = userManager;
            this.hubContext = articlehub;
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
            var user = await IdentityService.GetUserWithToken(userManager, HttpContext);

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
            var user = await IdentityService.GetUserWithToken(userManager, HttpContext);

            var article = await context.Article.FindAsync(comment.ArticleID);
            logger.Log($"{user.UserName} attempts to delete his comment `{comment.Text}` on the article of title `{article.Title}`");

            if (comment.UserID != user.Id)
            {
                return ValidationProblem("Only the owner of this comment is allowed to delete it.");
            }

            context.Comment.Remove(comment);
            await context.SaveChangesAsync();

            logger.Log("Delete Completed");

            return comment;
        }
    }
}
