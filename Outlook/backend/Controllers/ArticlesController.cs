using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Models.Interfaces;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using backend.Areas.Identity;

namespace backend.Controllers
{
    [Authorize(Roles = "Web-Editor, Editor-In-Chief, Admin")]
    public class ArticlesController : Controller
    {
        private readonly OutlookContext context;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;
        public static int VolumeNumber;
        public static int IssueNumber;

        public static List<string> Writers;
        public static List<string> Categories;

        public ArticlesController(OutlookContext context, IWebHostEnvironment env, IConfiguration config)
        {
            this.context = context;
            this.env = env;
            this.config = config;
        }

        // GET: Articles
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Save the Issue Number to be accessed
            var issue = from _issue in context.Issue
                        where _issue.Id == id
                        select _issue;

            // Save the Volume Number to be accessed
            var volume = from _volume in context.Volume
                         where _volume.Id == issue.FirstOrDefault().VolumeID
                         select _volume;


            if (issue.FirstOrDefault() != null && volume.FirstOrDefault() != null)
            {
                IssueNumber = issue.FirstOrDefault().IssueNumber;
                VolumeNumber = volume.FirstOrDefault().VolumeNumber;
            }

            // Save the List of wrtiers names to be accessed
            var writers = from member in context.Member
                          select member.Name;

            Writers = await writers.ToListAsync();

            // Save the List of categories names to be accessed
            var categories = from category in context.Category
                             select category.CategoryName;

            Categories = await categories.ToListAsync();

            var articles = from article in context.Article
                           where article.IssueID == id
                           select article;

            foreach (var article in articles)
            {
                var writer = context.Member.FirstOrDefault(m => m.ID == article.MemberID);
                var category = context.Category.FirstOrDefault(c => c.Id == article.CategoryID);
                if (writer != null)
                {
                    var writerName = writer.Name;
                    article.Writer = writerName;
                }
                if (category != null)
                {
                    var categoryName = category.CategoryName;
                    article.Category = categoryName;
                }
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

            // Get article's writer
            var writer = context.Member.First(m => m.ID == article.MemberID).Name;
            article.Writer = writer;
            
            // Get article's category
            var category = context.Category.First(c => c.Id == article.CategoryID).CategoryName;
            article.Category = category;

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
        public async Task<IActionResult> Create([FromRoute]int? id, [Bind("Language,Category,Title,Subtitle,Writer,NewWriter,Picture,Text")] Article article)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return ValidationProblem(detail: "Issue Id cannot be null");
                }
                // Assign value to the IssueID that refers to the Issue of the article
                article.IssueID = (int) id;

                // Save the date where the article was uploaded
                article.DateTime = DateTime.Now;

                Member writer;
                if (article.Writer != "New Writer")
                {
                    // Assign value to the MemberID that refers to the writer of the article
                    writer = context.Member.First(m => m.Name == article.Writer);
                }
                else
                {
                    writer = new Member { Name = article.NewWriter };

                    if (Regex.IsMatch(article.NewWriter, "^[a-zA-Z0-9. ]*$"))
                    {
                        writer.Position = Position.Staff_Writer;
                    }
                    else
                    {
                        writer.Position = Position.كاتب_صحفي;
                    }
                    context.Member.Add(writer);
                    await context.SaveChangesAsync();
                }
                var writerID = writer.ID;
                article.MemberID = writerID;
                writer.NumberOfArticles++;

                // Assign value to the MemberID that reefers to the writer of the article
                var categoryID = context.Category.First(c => c.CategoryName == article.Category).Id;
                article.CategoryID = categoryID;

                if (article.Picture != null)
                {
                    // Add unique name to avoid possible name conflicts
                    var uniqueImageName = DateTime.Now.Ticks.ToString() + ".jpg";
                    var articleImageFolderPath = Path.Combine(new string[] { env.WebRootPath, "img", "Articles\\" });
                    var articleImageFilePath = Path.Combine(articleImageFolderPath, uniqueImageName);
                    if (!Directory.Exists(articleImageFolderPath))
                    {
                        Directory.CreateDirectory(articleImageFolderPath);
                    }
                    using (var fileStream = new FileStream(articleImageFilePath, FileMode.Create, FileAccess.Write))
                    {
                        // Copy the photo to storage
                        await article.Picture.CopyToAsync(fileStream);
                    }
                    // Save picture local path in the article object
                    article.PicturePath = @"/img/Articles/"+ uniqueImageName;
                }
                
                context.Add(article);
                await context.SaveChangesAsync();

                FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"{HttpContext.User.Identity.Name} created article of title `{article.Title}` and ID `{article.Id}`");
                
                return RedirectToAction(nameof(Index), new { id = id});
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

            // Get article's writer
            var writer = context.Member.First(m => m.ID == article.MemberID).Name;
            article.Writer = writer;

            // Get article's category
            var category = context.Category.First(c => c.Id == article.CategoryID).CategoryName;
            article.Category = category;

            //// Retrieve the article image form the file server if there is any
            //if (article.PicturePath != null)
            //{
            //    var path = env.WebRootPath + article.PicturePath;
            //    using (var fileStream = System.IO.File.OpenRead(path))
            //    {
            //        var file = new FormFile(fileStream, 0, fileStream.Length, null, fileStream.Name)
            //        {
            //            Headers = new HeaderDictionary(),
            //            ContentType = "image/jpg",
            //            ContentDisposition = $"form-data; name=\"Picture\"; filename=\"{article.PictureName}\""
            //        };
            //        article.Picture = file;
            //    }
            //}
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Language,Category,Title,Subtitle,Writer,Picture,DeletePicture,Text")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var oldVersionArticle = context.Article.First(a => a.Id == id);
                
                try
                {
                   

                    string startLogMessage = await GetEditLogMessage(oldVersionArticle);

                    FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"This article is edited from `{startLogMessage}`");


                    // Update the value to the MemberID that refers to the writer of the article
                    Member writer;
                    if (article.Writer != "New Writer")
                    {
                        writer = context.Member.First(m => m.Name == article.Writer);
                    }
                    else
                    {
                        // Create a new writer if needed
                        writer = new Member { Name = article.NewWriter };

                        // Decide whether the writer writes for the English section or the Arabic section
                        if (Regex.IsMatch(article.NewWriter, "^[a-zA-Z0-9. ]*$"))
                        {
                            writer.Position = Position.Staff_Writer;
                        }
                        else
                        {
                            writer.Position = Position.كاتب_صحفي;
                        }
                        context.Member.Add(writer);
                        await context.SaveChangesAsync();
                    }
                    var writerID = writer.ID;
                    article.MemberID = writerID;

                    // Update the value to the MemberID that reefers to the writer of the article
                    var categoryID = context.Category.First(c => c.CategoryName == article.Category).Id;
                    article.CategoryID = categoryID;

                    oldVersionArticle.UpdateArticleInfo(article.Language, categoryID, article.Title, article.Subtitle, writerID, article.Text);

                    if (oldVersionArticle.PicturePath == null)
                    {
                        if (article.Picture != null)
                        {
                            // Add unique name to avoid possible name conflicts
                            var uniqueImageName = DateTime.Now.Ticks.ToString() + ".jpg";
                            var articleImageFilePath = Path.Combine(new string[] { env.WebRootPath, "img", "Articles", uniqueImageName });
                            using (var fileStream = new FileStream(articleImageFilePath, FileMode.Create, FileAccess.Write))
                            {
                                // Copy the photo to storage
                                await article.Picture.CopyToAsync(fileStream);
                            }
                            // Save picture local path in the article object
                            article.PicturePath = @"/img/Articles/" + uniqueImageName;
                        }
                    }
                    else
                    {
                        if (article.Picture != null)
                        {
                            // Delete the old picture from the server
                            var path = env.WebRootPath + oldVersionArticle.PicturePath;
                            System.IO.File.Delete(path);

                            // Copy the new picture to the server
                            var uniqueImageName = DateTime.Now.Ticks.ToString() + ".jpg";
                            var articleImageFilePath = Path.Combine(new string[] { env.WebRootPath, "img", "Articles", uniqueImageName });
                            using (var fileStream = new FileStream(articleImageFilePath, FileMode.Create, FileAccess.Write))
                            {
                                // Copy the photo to storage
                                await article.Picture.CopyToAsync(fileStream);
                            }
                            // Save picture local path in the article object
                            oldVersionArticle.PicturePath = @"/img/Articles/" + uniqueImageName;
                        }
                        else if (article.DeletePicture)
                        {
                            // Delete the old picture from the server
                            var path = env.WebRootPath + oldVersionArticle.PicturePath;
                            System.IO.File.Delete(path);

                            oldVersionArticle.PicturePath = null;
                        }
                    }

                    string endLogMessage = await GetEditLogMessage(oldVersionArticle);

                    FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"to: {endLogMessage} by {HttpContext.User.Identity.Name}");

                    //context.Update(article);
                    await context.SaveChangesAsync();
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

            var article = await context.Article
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            // Get article's writer
            var writer = context.Member.First(m => m.ID == article.MemberID).Name;
            article.Writer = writer;

            // Get article's category
            var category = context.Category.First(c => c.Id == article.CategoryID).CategoryName;
            article.Category = category;

            return View(article);
        }

        // POST: Articles/Delete/5
        [Authorize(Roles = "Editor-In-Chief, Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await context.Article.FindAsync(id);

            // Decrement the number of articles for the writer
            var writer = context.Member.First(m => m.ID == article.MemberID);
            writer.NumberOfArticles--;

            // Delete the article image form the file server if there is any
            if (article.PicturePath != null)
            {
                var path = env.WebRootPath + article.PicturePath;
                System.IO.File.Delete(path);
            }

            FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"{HttpContext.User.Identity.Name} attempts to delete article of title `{article.Title}` and ID `{article.Id}`.");

            context.Article.Remove(article);
            await context.SaveChangesAsync();
            
            FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"Delete Completed.");
            
            return RedirectToAction(nameof(Index), new { id = article.IssueID});
        }

        private bool ArticleExists(int id)
        {
            return context.Article.Any(e => e.Id == id);
        }

        private async Task<string> GetEditLogMessage(Article article)
        {
            var Writer = await context.Member.FindAsync(article.MemberID);
            var Category = await context.Category.FindAsync(article.CategoryID);

            return $"Title: {article.Title}\n" +
                $"Subtitle: {article.Subtitle}\n" +
                $"Category: {Category.CategoryName}" +
                $"Writer: {Writer.Name}\n" +
                $"Picture Name: {Path.GetFileName(article.PicturePath)}" +
                $"Body: {article.Text}\n";
        }
    }
}
