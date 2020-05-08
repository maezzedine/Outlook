using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
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
            Writers = context.Member.Select(m => m.Name).ToList();

            // Save the List of categories names to be accessed
            Categories = context.Category.Select(c => c.CategoryName).ToList();
        }

        // GET: Articles
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Save the Issue Number to be accessed
            var issue = await context.Issue.FindAsync(id);

            // Save the Volume Number to be accessed
            var volume = await context.Volume.FindAsync(issue.VolumeID);

            if (issue != null && volume != null)
            {
                Issue = issue;
                VolumeNumber = volume.VolumeNumber;
            }

            var articles = from article in context.Article
                           where article.IssueID == id
                           select article;

            foreach (var article in articles)
            {
                var writer = context.Member.FirstOrDefault(m => m.ID == article.MemberID);
                var category = context.Category.FirstOrDefault(c => c.Id == article.CategoryID);
            }

            return View(await articles.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await context.Article
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            articleService.GetArticleWriterAndCategory(article);

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
        public async Task<IActionResult> Create([FromRoute]int? id, [Bind("Language,Category,Title,Subtitle,Member,NewWriter,Picture,Text")] Article article)
        {
            ModelState.Remove("Category.CategoryName");
            ModelState.Remove("Member.Name");

            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return ValidationProblem(detail: "Issue Id cannot be null");
                }
                // Assign value to the IssueID that refers to the Issue of the article
                article.IssueID = (int)id;

                // Save the date where the article was uploaded
                article.DateTime = DateTime.Now;

                // Assign value to the MemberID that refers to the writer of the article
                article.Category = context.Category.First(c => c.CategoryName == article.Category.CategoryName);

                await articleService.EditArticleWriter(article);

                if (article.Picture != null)
                {
                    await articleService.AddArticlePicture(article, article.Picture, env.WebRootPath);
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

            var article = await context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            articleService.GetArticleWriterAndCategory(article);

            return View(article);
        }

        // POST: Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Language,Category,Title,Subtitle,Member,Picture,DeletePicture,Text")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Category.CategoryName");
            ModelState.Remove("Member.Name");

            if (ModelState.IsValid)
            {
                var oldVersionArticle = context.Article.First(a => a.Id == id);

                try
                {
                    string startLogMessage = GetEditLogMessage(oldVersionArticle);

                    logger.Log($"This article is edited from `{startLogMessage}`");

                    await articleService.EditArticleWriter(oldVersionArticle);

                    // Update the value to the MemberID that refers to the writer of the article
                    oldVersionArticle.Category = context.Category.First(c => c.CategoryName == article.Category.CategoryName);

                    articleService.UpdateArticleInfo(article, article.Language, article.Title, article.Subtitle, article.Text);

                    if (oldVersionArticle.PicturePath == null)
                    {
                        if (article.Picture != null)
                        {
                            await articleService.AddArticlePicture(oldVersionArticle, article.Picture, env.WebRootPath);
                        }
                    }
                    else
                    {
                        if (article.Picture != null)
                        {
                            articleService.DeleteArticlePicture(oldVersionArticle, env.WebRootPath);

                            await articleService.AddArticlePicture(oldVersionArticle, article.Picture, env.WebRootPath);
                        }
                        else if (article.DeletePicture)
                        {
                            articleService.DeleteArticlePicture(oldVersionArticle, env.WebRootPath);
                        }
                    }

                    await context.SaveChangesAsync();

                    string endLogMessage = GetEditLogMessage(oldVersionArticle);
                    logger.Log($"to: {endLogMessage} by {HttpContext.User.Identity.Name}");
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
                return RedirectToAction(nameof(Index), new { id = oldVersionArticle.IssueID });
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

            var article = await context.Article.FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            articleService.GetArticleWriterAndCategory(article);

            return View(article);
        }

        // POST: Articles/Delete/5
        [Authorize(Roles = "Editor-In-Chief, Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await context.Article.FindAsync(id);

            // Delete the article image form the file server if there is any
            if (article.PicturePath != null)
            {
                articleService.DeleteArticlePicture(article, env.WebRootPath);
            }

            logger.Log($"{HttpContext.User.Identity.Name} attempts to delete article of title `{article.Title}` and ID `{article.Id}`.");

            context.Article.Remove(article);
            await context.SaveChangesAsync();

            logger.Log($"Delete Completed.");

            return RedirectToAction(nameof(Index), new { id = article.IssueID });
        }

        private bool ArticleExists(int id)
        {
            return context.Article.Any(e => e.Id == id);
        }

        private string GetEditLogMessage(Article article)
        {
            articleService.GetArticleWriterAndCategory(article);

            return $"Title: {article.Title}\n" +
                $"Subtitle: {article.Subtitle}\n" +
                $"Category: {article.Category.CategoryName}" +
                $"Writer: {article.Member.Name}\n" +
                $"Picture Name: {Path.GetFileName(article.PicturePath)}" +
                $"Body: {article.Text}\n";
        }
    }
}
