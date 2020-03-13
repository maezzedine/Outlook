using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Models.Interfaces;
using System.Text.RegularExpressions;

namespace backend.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly OutlookContext context;

        public static int VolumeNumber;
        public static int IssueNumber;

        public static List<string> Writers;
        public static List<string> Categories;

        public ArticlesController(OutlookContext context)
        {
            this.context = context;
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

            var writer = context.Member.First(m => m.ID == article.MemberID).Name;
            var category = context.Category.First(c => c.Id == article.CategoryID).CategoryName;
            article.Writer = writer;
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
                
                context.Add(article);
                await context.SaveChangesAsync();
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

            var writer = context.Member.First(m => m.ID == article.MemberID).Name;
            var category = context.Category.First(c => c.Id == article.CategoryID).CategoryName;
            article.Writer = writer;
            article.Category = category;

            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Language,Category,Title,Subtitle,Writer,Picture,Text")] Article article)
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
                    // Update the value to the MemberID that reefers to the writer of the article
                    Member writer;
                    if (article.Writer != "New Writer")
                    {
                        writer = context.Member.First(m => m.Name == article.Writer);
                    }
                    else
                    {
                        // Create a new writer if needed
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

                    // Update the value to the MemberID that reefers to the writer of the article
                    var categoryID = context.Category.First(c => c.CategoryName == article.Category).Id;
                    article.CategoryID = categoryID;

                    oldVersionArticle.UpdateArticleInfo(article.Language, categoryID, article.Title, article.Subtitle, writerID, article.Picture, article.Text);

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

            var writer = context.Member.First(m => m.ID == article.MemberID).Name;
            var category = context.Category.First(c => c.Id == article.CategoryID).CategoryName;
            article.Writer = writer;
            article.Category = category;

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await context.Article.FindAsync(id);

            // Decrement the number of articles for the writer
            var writer = context.Member.First(m => m.ID == article.MemberID);
            writer.NumberOfArticles--;

            context.Article.Remove(article);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = article.IssueID});
        }

        private bool ArticleExists(int id)
        {
            return context.Article.Any(e => e.Id == id);
        }
    }
}
