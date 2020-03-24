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
    public class RepliesController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly IConfiguration config;

        public RepliesController(OutlookContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        // GET: api/Replies
        [HttpGet("{commentID}")]
        public async Task<ActionResult<IEnumerable<Reply>>> GetReplies(int commentID)
        {
            var replies = from reply in context.Reply
                          where reply.CommentID == commentID
                          select reply;

            return await replies.ToListAsync();
        }

        // GET: api/Replies/5
        [HttpGet("Reply/{id}")]
        public async Task<ActionResult<Reply>> GetReply(int id)
        {
            var reply = await context.Reply.FindAsync(id);

            if (reply == null)
            {
                return NotFound();
            }

            return reply;
        }

        // PUT: api/Replies/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReply(int id, Reply reply)
        {
            if (id != reply.Id)
            {
                return BadRequest();
            }

            context.Entry(reply).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var user = await context.Users.FindAsync(reply.UserID);
            var comment = await context.Comment.FindAsync(reply.CommentID);
            var article = await context.Article.FindAsync(comment.ArticleID);

            FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"{DateTime.Now} | {user.UserName} editted his reply `{reply.Text}` on the comment `{comment.Text}` on the article of title `{article.Title}`");

            return NoContent();
        }

        // POST: api/Replies
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<Reply>> PostReply(Reply reply)
        {
            context.Reply.Add(reply);
            await context.SaveChangesAsync();

            var user = await context.Users.FindAsync(reply.UserID);
            var comment = await context.Comment.FindAsync(reply.CommentID);
            var article = await context.Article.FindAsync(comment.ArticleID);

            FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"{DateTime.Now} | {user.UserName} replied by `{reply.Text}` on the comment `{comment.Text}` on the article of title `{article.Title}`");


            return CreatedAtAction("GetReply", new { id = reply.Id }, reply);
        }

        // DELETE: api/Replies/5
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reply>> DeleteReply(int id)
        {
            var reply = await context.Reply.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            }

            var user = await context.Users.FindAsync(reply.UserID);
            var comment = await context.Comment.FindAsync(reply.CommentID);
            var article = await context.Article.FindAsync(comment.ArticleID);

            FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"{DateTime.Now} | {user.UserName} admits to delete his reply `{reply.Text}` on the comment `{comment.Text}` on the article of title `{article.Title}`");

            context.Reply.Remove(reply);
            await context.SaveChangesAsync();

            FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"Delete Completed.");


            return reply;
        }

        private bool ReplyExists(int id)
        {
            return context.Reply.Any(e => e.Id == id);
        }
    }
}
