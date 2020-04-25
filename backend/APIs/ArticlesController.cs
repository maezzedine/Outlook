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
using Microsoft.AspNetCore.SignalR;
using backend.Hubs;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly UserManager<OutlookUser> userManager;
        private readonly IHubContext<ArticleHub, IArticleHub> hubContext;

        public ArticlesController(OutlookContext context, UserManager<OutlookUser> userManager, IHubContext<ArticleHub, IArticleHub> articlehub)
        {
            this.context = context;
            this.userManager = userManager;
            this.hubContext = articlehub;
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

            var userRateArticle = await context.UserRateArticle.FirstOrDefaultAsync(u => (u.Article.Id == articleID) && (u.User.Id == user.Id));

            var article = await context.Article.FindAsync(articleID);

            if (userRateArticle != null && userRateArticle.Rate == UserRate.Up)
            {
                return ValidationProblem(detail: "You've already rated up this article.");
            }

            if (userRateArticle != null && userRateArticle.Rate == UserRate.Down)
            {
                userRateArticle.Rate = UserRate.Up;
                article.UnRateDown();
            }

            // Rate up the article
            context.UserRateArticle.Add(new UserRateArticle { User = user, Article = article, Rate = UserRate.Up });
            article.RateUp();

            // Notify all clients
            await this.hubContext
                .Clients
                .All
                .ArticleScoreChange(article.Id, article.Rate, article.NumberOfVotes);

            await context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("RateDownArticle/{articleID}")]
        public async Task<ActionResult> RateDownArticle(int articleID)
        {
            var username = HttpContext.User.FindFirst("name")?.Value;
            var user = await userManager.FindByNameAsync(username);

            var userRateArticle = await context.UserRateArticle.FirstOrDefaultAsync(u => (u.Article.Id == articleID) && (u.User.Id == user.Id));


            var article = await context.Article.FindAsync(articleID);

            if (userRateArticle != null && userRateArticle.Rate == UserRate.Down)
            {
                return ValidationProblem(detail: "You've already rated down this article.");
            }

            if (userRateArticle != null && userRateArticle.Rate == UserRate.Up)
            {
                userRateArticle.Rate = UserRate.Down;
                article.UnRateUp();
            }

            // Rate down the article
            context.UserRateArticle.Add(new UserRateArticle { User = user, Article = article, Rate = UserRate.Down });
            article.RateDown();

            // Notify all clients
            await this.hubContext
                .Clients
                .All
                .ArticleScoreChange(article.Id, article.Rate, article.NumberOfVotes);

            await context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("FavoriteArticle/{articleID}")]
        public async Task<ActionResult> FavoriteArticle(int articleID)
        {
            var username = HttpContext.User.FindFirst("name")?.Value;
            var user = await userManager.FindByNameAsync(username);

            var userFavoritedArticle = await context.UserFavoritedArticleRelation.FirstOrDefaultAsync(u => (u.Article.Id == articleID) && (u.User.Id == user.Id));

            var article = await context.Article.FindAsync(articleID);

            if (userFavoritedArticle != null)
            {
                context.UserFavoritedArticleRelation.Remove(userFavoritedArticle);
                article.NumberOfFavorites--;
            }
            else
            {
                context.UserFavoritedArticleRelation.Add(new UserFavoritedArticleRelation { User = user, Article = article });
                article.NumberOfFavorites++;
            }

            await context.SaveChangesAsync();
            
            // Notify all clients
            await hubContext.Clients.All.ArticleFavoriteChange(article.Id, article.NumberOfFavorites);

            return Ok();
        }

        public async Task GetArticleProperties(Article article)
        {
            // Add the category
            var category = await context.Category.FindAsync(article.CategoryID);
            article.CategoryTagName = category.Tag.ToString();

            // Add the writer
            var writer = await context.Member.FindAsync(article.MemberID);

            // Add the langauge
            article.Lang = (article.Language == Models.Interfaces.Language.English) ? "en" : "ar";

            // Add the comment list on the article
            var comments = from comment in context.Comment
                           where comment.ArticleID == article.Id
                           select comment;

            // Add replies list for each comment
            foreach (var comment in comments)
            {
                var owner = await context.Users.FindAsync(comment.UserID);
                comment.User = owner;
            }

            article.Comments = await comments.ToListAsync();
        }
    }
}
