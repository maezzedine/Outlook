using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Outlook.Api.Hubs;
using Outlook.Models.Core.Dtos;
using Outlook.Models.Core.Models;
using Outlook.Models.Data;
using Outlook.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Outlook.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly IdentityService identityService;
        private readonly IHubContext<ArticleHub, IArticleHub> articleHub;
        private readonly IMapper mapper;
        private readonly Logger.Logger logger;

        public CommentsController(
            OutlookContext context,
            IdentityService identityService,
            IMapper mapper,
            IHubContext<ArticleHub, IArticleHub> articleHub)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
            this.articleHub = articleHub;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.web);
        }

        public class PostCommentModel
        {
            public int articleId;
            public string text;
        }

        /// <summary>
        /// Add a comment on a specific article given its ID and the Bearer token of the user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/comments
        ///     {
        ///         "Content-Type": "application/json",
        ///         "Authorization": `Bearer ${token}`,
        ///         "body": {
        ///             "ArticleId": 1,
        ///             "Text": "hello world!"
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <param name="_comment"></param>
        /// <response code="201">Comment has successfully bben added</response>
        /// <response code="401">The user is unauthorized</response>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment _comment)
        {
            var user = await identityService.GetUserWithToken(HttpContext);

            var comment = new Comment() { ArticleID = _comment.ArticleID, Text = _comment.Text, DateTime = DateTime.Now, UserID = user.Id };

            context.Comment.Add(comment);
            await context.SaveChangesAsync();

            var article = await context.Article.FindAsync(comment.ArticleID);

            var comments = await context.Comment
                .Where(c => c.ArticleID == article.Id)
                .Include(c => c.User)
                .Select(c => mapper.Map<CommentDto>(c))
                .ToListAsync();

            foreach (var comment_ in comments)
            {
                var owner = await context.Users.FindAsync(comment_.User.Id);
                comment_.User = owner;
            }

            await articleHub.Clients.All.ArticleCommentChange(article.Id, comments);

            logger.Log($"{user.UserName} posted a comment `{comment.Text}` on the article of title `{article.Title}`");

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        /// <summary>
        /// Delete a comment given its ID and the Bearer token of the user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/comments/1
        ///     {
        ///         "Content-Type": "application/json",
        ///         "Authorization": `Bearer ${token}`
        ///     }
        ///     
        /// </remarks>
        /// <param name="id">comment ID</param>
        /// <response code="200">The comment has successfully been deleted</response>
        /// <response code="400">The user is npt the owner of the comment</response>
        /// <response code="401">The user is unauthorized</response>
        /// <response code="404">Returns NotFound result if no comment with the given ID was found</response>
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

            var comments = await context.Comment
                .Where(c => c.ArticleID == article.Id)
                .Include(c => c.User)
                .Select(c => mapper.Map<CommentDto>(c))
                .ToListAsync();

            await articleHub.Clients.All.ArticleCommentChange(article.Id, comments);

            return Ok();
        }
    }
}
