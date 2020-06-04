using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Outlook.Models.Core.Models;
using Outlook.Models.Data;
using Outlook.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Outlook.Server.Controllers
{
    [Authorize(Roles = "Web-Editor, Editor-In-Chief, Admin", AuthenticationSchemes = "Identity.Application")]
    public class ArticlesController : Controller
    {
        private readonly OutlookContext context;
        private readonly IWebHostEnvironment env;
        private readonly Logger.Logger logger;
        private readonly ArticleService articleService;

        public static int VolumeNumber;
        public static Issue Issue;
        public static List<string> Writers;
        public static List<string> Categories;

        public ArticlesController(
            OutlookContext context,
            IWebHostEnvironment env,
            ArticleService articleService)
        {
            this.context = context;
            this.env = env;
            this.articleService = articleService;

            logger = Logger.Logger.Instance(Logger.Logger.LogField.server);

            // Save the List of wrtiers names to be accessed
            Writers = context.Member
                .Select(m => m.Name)
                .ToList();

            // Save the List of categories names to be accessed
            Categories = context.Category
                .Select(c => c.Name)
                .ToList();
        }

        // GET: Articles
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Save the Issue Number to be accessed
            var issue = await context.Issue
                .Include(i => i.Volume)
                .FirstOrDefaultAsync(i => i.Id == id);

            // Save the Volume Number to be accessed
            var volume = await context.Volume
                .FindAsync(issue.Volume.Id);

            if (issue != null && volume != null)
            {
                Issue = issue;
                VolumeNumber = volume.Number;
            }

            var articles = await context.Article
                .Include(a => a.Issue)
                .Include(a => a.Category)
                .Include(a => a.Writer)
                .Where(a => a.Issue.Id == id)
                .ToListAsync();

            return View(articles);
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await context.Article
                .Include(a => a.Issue)
                .Include(a => a.Category)
                .Include(a => a.Writer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromRoute] int? id, string NewWriter, IFormFile Picture, [Bind("Language,Category,Writer,Title,Subtitle,Text")] Article article)
        {
            ModelState.Remove("Category.Name");
            ModelState.Remove("Writer.Name");

            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return ValidationProblem(detail: "Issue Id cannot be null");
                }

                article.Issue = await context.Issue.FindAsync(id);
                article.DateTime = DateTime.Now;

                // Assign value to the MemberID that refers to the writer of the article
                article.Category = context.Category
                    .First(c => c.Name == article.Category.Name);

                articleService.SetArticleWriter(article,
                    (article.Writer.Name == "+ NEW WRITER") ? NewWriter : article.Writer.Name);

                if (Picture != null)
                {
                    await articleService.AddArticlePicture(article, Picture, env.WebRootPath);
                }

                context.Add(article);
                await context.SaveChangesAsync();

                logger.Log($"{HttpContext.User.Identity.Name} created article of title `{article.Title}` and ID `{article.Id}`");

                return RedirectToAction(nameof(Index), new { id = id });
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await context.Article
                .Include(i => i.Issue)
                .Include(a => a.Category)
                .Include(a => a.Writer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string NewWriter, bool DeletePicture, IFormFile Picture, Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Category.Name");
            ModelState.Remove("Writer.Name");
            ModelState.Remove("DeletePicture");

            if (ModelState.IsValid)
            {
                var originalArticle = context.Article
                    .Include(i => i.Issue)
                    .Include(a => a.Category)
                    .Include(a => a.Writer)
                    .First(a => a.Id == id);

                try
                {
                    string startLogMessage = GetEditLogMessage(originalArticle);

                    articleService
                        .SetArticleWriter(originalArticle,
                            (article.Writer.Name == "+ NEW WRITER") ? NewWriter : article.Writer.Name)
                        .SetLanguage(article.Language)
                        .SetText(article.Text)
                        .SetTitle(article.Title)
                        .SetSubtitle(article.Subtitle)
                        .SetCategory(context.Category
                                        .First(c => c.Name == article.Category.Name));

                    if (originalArticle.PicturePath == null)
                    {
                        if (Picture != null)
                        {
                            await articleService.AddArticlePicture(originalArticle, Picture, env.WebRootPath);
                        }
                    }
                    else
                    {
                        if (Picture != null)
                        {
                            articleService.DeleteArticlePicture(originalArticle, env.WebRootPath);

                            await articleService.AddArticlePicture(originalArticle, Picture, env.WebRootPath);
                        }
                        else if (DeletePicture)
                        {
                            articleService.DeleteArticlePicture(originalArticle, env.WebRootPath);
                        }
                    }

                    await context.SaveChangesAsync();

                    string endLogMessage = GetEditLogMessage(originalArticle);

                    logger.Log($@"This article is edited 
                                from `{startLogMessage}`
                                to: {endLogMessage}
                                by {HttpContext.User.Identity.Name}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = originalArticle.Issue.Id });
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Editor-In-Chief, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await context.Article
               .Include(i => i.Issue)
               .Include(a => a.Category)
               .Include(a => a.Writer)
               .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [Authorize(Roles = "Editor-In-Chief, Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await context.Article
                .Include(a => a.Issue)
                .FirstOrDefaultAsync(a => a.Id == id);

            // Delete the article image form the file server if there is any
            if (article.PicturePath != null)
            {
                articleService.DeleteArticlePicture(article, env.WebRootPath);
            }

            logger.Log($"{HttpContext.User.Identity.Name} attempts to delete article of title `{article.Title}` and ID `{article.Id}`.");

            context.Article.Remove(article);
            await context.SaveChangesAsync();

            logger.Log($"Delete Completed.");

            return RedirectToAction(nameof(Index), new { id = article.Issue.Id });
        }

        private bool ArticleExists(int id)
        {
            return context.Article.Any(e => e.Id == id);
        }

        private string GetEditLogMessage(Article article)
        {
            return $@"Title: {article.Title}
                    Subtitle: {article.Subtitle}
                    Category: {article.Category.Name}
                    Writer: {article.Writer.Name}
                    Picture Name: {Path.GetFileName(article.PicturePath)}
                    Body: {article.Text}";
        }
    }
}
