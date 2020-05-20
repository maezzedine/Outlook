using AutoMapper;
using backend.Data;
using backend.Hubs;
using backend.Models.Dtos;
using backend.Models.Interfaces;
using backend.Models.Relations;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly IdentityService identityService;
        private readonly IWebHostEnvironment env;
        private readonly IHubContext<ArticleHub, IArticleHub> hubContext;
        private readonly Logger.Logger logger;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public ArticlesController(
            OutlookContext context,
            IMapper mapper,
            IdentityService identityService,
            IHubContext<ArticleHub, IArticleHub> articlehub,
            IWebHostEnvironment env, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.identityService = identityService;
            this.env = env;
            this.configuration = configuration;
            hubContext = articlehub;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.userArticles);
        }

        /// <summary>
        /// Get the list of articles (in all languages) in a specific issue
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/articles/1
        /// 
        /// </remarks>
        /// <param name="issueID"></param>
        /// <returns>List of articles</returns>
        /// <response code="200">Returns the list of articles in the issue given its ID</response>
        [HttpGet("{issueID}")]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticles(int issueID)
        {
            // Get articles in an issue
            var articles = context.Article
                .Where(a => a.IssueID == issueID)
                .Include(a => a.Category)
                .Include(a => a.Issue)
                .Include(a => a.Member)
                .Include(a => a.Comments)
                .ThenInclude(c => c.User)
                .Select(a => mapper.Map<ArticleDto>(a));

            return await articles.ToListAsync();
        }

        /// <summary>
        /// Get a specific article 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/articles/article/1
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>An article given its ID</returns>
        /// <response code="200">Returns the article</response>
        /// <response code="404">Returns NotFound result if no article with the given ID was found</response>
        [HttpGet("Article/{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticle(int id)
        {
            var article = await context.Article
                .Include(a => a.Category)
                .Include(a => a.Issue)
                .Include(a => a.Member)
                .Include(a => a.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return mapper.Map<ArticleDto>(article);
        }

        /// <summary>
        /// Gets the statistics of the top rated and the most favorited articles in both languages
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/articles
        /// 
        /// </remarks>
        /// <returns>A JSON Result of the keys topRatedArticles and topFavoritedArticles</returns>
        /// <reponse code="200">JSON object containing a list of the top rated articles and a list of the most favorited articles</reponse>
        [HttpGet]
        public async Task<ActionResult> GetTopArticles()
        {
            var topRatedArticles = await context.Article
                .Include(a => a.Category)
                .Include(a => a.Issue)
                .Include(a => a.Member)
                .OrderByDescending(a => a.Rate)
                .Select(a => mapper.Map<ArticleDto>(a))
                .ToListAsync();

            var topFavoritedArticles = await context.Article
                .Include(a => a.Category)
                .Include(a => a.Issue)
                .Include(a => a.Member)
                .OrderByDescending(a => a.NumberOfFavorites)
                .Select(a => mapper.Map<ArticleDto>(a))
                .ToListAsync();

            var shortListedTopRatedArticles = GetShortlistedTopArticles(topRatedArticles);
            var shortListedTopFavoritedArticles = GetShortlistedTopArticles(topFavoritedArticles);

            return Ok(new
            {
                topRatedArticles = shortListedTopRatedArticles,
                topFavoritedArticles = shortListedTopFavoritedArticles
            });
        }

        /// <summary>
        /// Upvotes an article given its ID and the Bearer token of the user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/articles/rateuparticle/1
        ///     {
        ///         "Content-Type": "application/json",
        ///         "Authorization": `Bearer ${token}`
        ///     }
        /// 
        /// </remarks>
        /// <param name="articleID"></param>
        /// <response code="200">The article has been upvoted successfully by the user</response>
        /// <response code="202">The article is already upvoted by the user</response>
        /// <response code="401">The user is unauthorized</response>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("RateUpArticle/{articleID}")]
        public async Task<ActionResult> RateUpArticle(int articleID)
        {
            var user = await identityService.GetUserWithToken(HttpContext);
            var userRateArticle = await context.UserRateArticle.FirstOrDefaultAsync(u => (u.Article.Id == articleID) && (u.User.Id == user.Id));
            var article = await context.Article.FindAsync(articleID);

            if (userRateArticle != null)
            {
                if (userRateArticle.Rate == UserRateArticle.UserRate.Up)
                {
                    return Accepted("You've already rated up this article.");
                }
                else
                {
                    if (userRateArticle.Rate == UserRateArticle.UserRate.Down)
                    {
                        article.UnRateDown();
                    }
                    userRateArticle.Rate = UserRateArticle.UserRate.Up;
                }
            }
            else
            {
                // Rate up the article
                context.UserRateArticle.Add(new UserRateArticle { User = user, Article = article, Rate = UserRateArticle.UserRate.Up });
            }
            article.RateUp();
            await context.SaveChangesAsync();

            // Notify all clients
            await hubContext.Clients.All.ArticleScoreChange(article.Id, article.Rate, article.NumberOfVotes);

            return Ok();
        }

        /// <summary>
        /// Downvotes an article given its ID and the Bearer token of the user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/articles/downuparticle/1
        ///     {
        ///         "Content-Type": "application/json",
        ///         "Authorization": `Bearer ${token}`
        ///     }
        /// 
        /// </remarks>
        /// <param name="articleID"></param>
        /// <response code="200">The article has been downvoted successfully by the user</response>
        /// <response code="202">The article is already downvoted by the user</response>
        /// <response code="401">The user is unauthorized</response>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("RateDownArticle/{articleID}")]
        public async Task<ActionResult> RateDownArticle(int articleID)
        {
            var user = await identityService.GetUserWithToken(HttpContext);
            var userRateArticle = await context.UserRateArticle.FirstOrDefaultAsync(u => (u.Article.Id == articleID) && (u.User.Id == user.Id));
            var article = await context.Article.FindAsync(articleID);

            if (userRateArticle != null)
            {
                if (userRateArticle.Rate == UserRateArticle.UserRate.Down)
                {
                    return Accepted("You've already rated down this article.");
                }
                else
                {
                    if (userRateArticle.Rate == UserRateArticle.UserRate.Up)
                    {
                        article.UnRateUp();
                    }
                    userRateArticle.Rate = UserRateArticle.UserRate.Down;
                }
            }
            else
            {
                // Rate down the article
                context.UserRateArticle.Add(new UserRateArticle { User = user, Article = article, Rate = UserRateArticle.UserRate.Down });
            }
            article.RateDown();
            await context.SaveChangesAsync();

            // Notify all clients
            await hubContext.Clients.All.ArticleScoreChange(article.Id, article.Rate, article.NumberOfVotes);

            return Ok();
        }

        /// <summary>
        /// Add or Remove an article, given its ID and the Bearer token of the user, to and from the user's favorite list
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/articles/favoritearticle/1
        ///     {
        ///         "Content-Type": "application/json",
        ///         "Authorization": `Bearer ${token}`
        ///     }
        /// 
        /// </remarks>
        /// <param name="articleID"></param>
        /// <response code="200">The article has been added/removed successfully to/from the user's favorite list</response>
        /// <response code="401">The user is unauthorized</response>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("FavoriteArticle/{articleID}")]
        public async Task<ActionResult> FavoriteArticle(int articleID)
        {
            var user = await identityService.GetUserWithToken(HttpContext);
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

        /// <summary>
        /// Gets a user's favorite list
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/article/getuserfavorites
        ///     {
        ///         "Content-Type": "application/json",
        ///         "Authorization": `Bearer ${token}`
        ///     }
        ///     
        /// </remarks>
        /// <returns>List of articles</returns>
        /// <response code="200">Returns the list of user's favorited articles</response>
        /// <response code="401">The user is unauthorized</response>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("GetUserFavorites")]
        public async Task<List<ArticleDto>> GetUserFavorites()
        {
            var user = await identityService.GetUserWithToken(HttpContext);

            var userFavoriteArticles = await context.UserFavoritedArticleRelation
                .Include(a => a.Article.Category)
                .Include(a => a.Article.Issue)
                .Include(a => a.Article.Member)
                .Where(a => a.User == user)
                .Select(a => mapper.Map<ArticleDto>(a.Article))
                .ToListAsync();

            return userFavoriteArticles;
        }

        /// <summary>
        /// Upload an article by the user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/article/uploadarticle, file.pdf
        ///     {
        ///         "Content-Type": "multipart/form-data",
        ///         "Authorization": `Bearer ${token}`,
        ///     }
        ///     
        /// </remarks>
        /// <param name="file">PDF document</param>
        /// <response code="200">Upload completed successfully</response>
        /// <response code="401">The user is unauthorized</response>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("Upload")]
        public async Task<ActionResult> UploadArticle(IFormCollection file)
        {
            var user = await identityService.GetUserWithToken(HttpContext);
            var aubDomains = new string[] { "aub.edu.lb", "mail.aub.edu" };

            if (!aubDomains.Contains(user.Email.Split('@')[1]))
            {
                return Unauthorized();
            }

            if (file.Files.Count != 0)
            {
                var document = file.Files.ElementAt(0);
                var extension = document.FileName.Substring(document.FileName.LastIndexOf('.'));

                // Add unique name to avoid possible name conflicts
                var uniqueDocumentName = $"{user.UserName}_{DateTime.Now.Ticks}.{extension}";
                var articleDocumentsFolderPath = Path.Combine(new string[] { env.WebRootPath, "docs", "Articles\\" });
                var articleDocumentFilePath = Path.Combine(articleDocumentsFolderPath, uniqueDocumentName);

                if (!Directory.Exists(articleDocumentsFolderPath))
                {
                    Directory.CreateDirectory(articleDocumentsFolderPath);
                }

                using (var fileStream = new FileStream(articleDocumentFilePath, FileMode.Create, FileAccess.Write))
                {
                    if (file.Files.Count != 0)
                    {
                        // Copy the photo to storage
                        await document.CopyToAsync(fileStream);
                    }
                }

                var baseUrl = configuration["ApplicationUrls:Server"];
                var ArticleDocumentFullPath = $"{baseUrl}/docs/Articles/{uniqueDocumentName}";
                logger.Log(ArticleDocumentFullPath);
            }

            return Ok();
        }

        private IEnumerable<ArticleDto> GetShortlistedTopArticles(List<ArticleDto> articles)
        {
            var englishShortlistedArticles = articles.Where(a => a.Language == Language.English.ToString()).AsEnumerable().Take(3);
            var arabicShortlistedArticles = articles.Where(a => a.Language == Language.Arabic.ToString()).AsEnumerable().Take(3);

            return englishShortlistedArticles.Concat(arabicShortlistedArticles);
        }
    }
}
