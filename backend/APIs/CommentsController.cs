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

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly IConfiguration config;

        public CommentsController(OutlookContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
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

            var user = await context.Users.FindAsync(comment.UserID);
            var article = await context.Article.FindAsync(comment.ArticleID);

            FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"{DateTime.Now} | {user.UserName} editted his comment `{comment.Text}` on the article of title `{article.Title}`");

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            context.Comment.Add(comment);
            await context.SaveChangesAsync();

            var user = await context.Users.FindAsync(comment.UserID);
            var article = await context.Article.FindAsync(comment.ArticleID);

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
