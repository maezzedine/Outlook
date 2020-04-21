using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using backend.Areas.Identity;
using backend.Models.Relations;
using static backend.Models.Relations.UserRateArticle;
using Microsoft.AspNetCore.Authorization;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly UserManager<OutlookUser> userManager;

        public ArticlesController(OutlookContext context, UserManager<OutlookUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
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

        // GET: api/Articles
        [HttpGet]
        public ActionResult GetTopArticles()
        {
            var topRatedArticles = from article in context.Article
                                   orderby article.NumberOfVotes
                                   descending
                                   select article;

            var topFavoritedArticles = from article in context.Article
                                       orderby article.NumberOfFavorites
                                       descending
                                       select article;

            var shortListedTopRatedArticles = topRatedArticles.Where(a => a.Language == Models.Interfaces.Language.English).AsEnumerable().Take(3)
                .Concat(topRatedArticles.Where(a => a.Language == Models.Interfaces.Language.Arabic).AsEnumerable().Take(3));
            
            var shortListedTopFavoritedArticles = topFavoritedArticles.Where(a => a.Language == Models.Interfaces.Language.English).AsEnumerable().Take(3)
                .Concat(topFavoritedArticles.Where(a => a.Language == Models.Interfaces.Language.Arabic).AsEnumerable().Take(3));

            return Ok(new
            {
                topRatedArticles = shortListedTopRatedArticles,
                topFavoritedArticles = shortListedTopFavoritedArticles
            });
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("RateUpArticle/{articleID}")]
        public async Task<ActionResult> RateUpArticle(int articleID)
        {
            var username = HttpContext.User.FindFirst("name")?.Value;
            var user = await userManager.FindByNameAsync(username);

            var userLikesArticle = await context.UserRateArticle.FirstOrDefaultAsync(u => (u.ArticleID == articleID) && (u.UserID == user.Id) && (u.Rate == UserRate.Up));
            var userDislikesArticle = await context.UserRateArticle.FirstOrDefaultAsync(u => (u.ArticleID == articleID) && (u.UserID == user.Id) && (u.Rate == UserRate.Down));

            var article = await context.Article.FindAsync(articleID);

            if (userLikesArticle != null)
            {
                return ValidationProblem(detail: "You've already rated up this article.");
            }

            if (userDislikesArticle != null)
            {
                context.UserRateArticle.Remove(userDislikesArticle);
                article.UnRateDown();
            }

            // Rate up the article
            context.UserRateArticle.Add(new UserRateArticle { UserID = user.Id, ArticleID = articleID, Rate = UserRate.Up });
            article.RateUp();

            await context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("RateDownArticle/{articleID}")]
        public async Task<ActionResult> RateDownArticle(int articleID)
        {
            var username = HttpContext.User.FindFirst("name")?.Value;
            var user = await userManager.FindByNameAsync(username);

            var userLikesArticle = await context.UserRateArticle.FirstOrDefaultAsync(u => (u.ArticleID == articleID) && (u.UserID == user.Id));
            var userDislikesArticle = await context.UserRateArticle.FirstOrDefaultAsync(u => (u.ArticleID == articleID) && (u.UserID == user.Id));

            var article = await context.Article.FindAsync(articleID);

            if (userDislikesArticle != null)
            {
                return ValidationProblem(detail: "You've already rated up this article.");
            }

            if (userLikesArticle != null)
            {
                context.UserRateArticle.Remove(userLikesArticle);
                article.UnRateUp();
            }

            // Rate down the article
            context.UserRateArticle.Add(new UserRateArticle { UserID = user.Id, ArticleID = articleID, Rate = UserRate.Down });
            article.RateDown();

            await context.SaveChangesAsync();

            return Ok();
        }

        public async Task GetArticleProperties(Article article)
        {
            // Add the category name
            var category = await context.Category.FindAsync(article.CategoryID);
            article.Category = category.CategoryName;
            article.CategoryTagName = category.Tag.ToString();

            // Add the writer name
            var writer = await context.Member.FindAsync(article.MemberID);
            article.Writer = writer.Name;
            article.WriterPosition = writer.PositionName;

            // Add the langauge
            article.Lang = (article.Language == Models.Interfaces.Language.English) ? "en" : "ar";

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

            //if (User.Identity.Name != null)
            //{
            //    var user = await userManager.FindByNameAsync(User.Identity.Name);

            //    // Add user rate to the article
            //    var userRatedArticle = await context.UserRateArticle.FirstOrDefaultAsync(r => (r.UserID == user.Id) && (r.ArticleID == article.Id));

            //    if (userRatedArticle == null)
            //    {
            //        article.RatedByUser = UserRate.None;
            //    }
            //    else
            //    {
            //        article.RatedByUser = userRatedArticle.Rate;
            //    }
            //}
        }
    }
}
