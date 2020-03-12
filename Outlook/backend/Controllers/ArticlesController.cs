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
                          where (member.Position == Position.Staff_Writer) || (member.Position == Position.كاتب_صحفي)
                          select member.Name;

            Writers = await writers.ToListAsync();

            // Save the List of categories names to be accessed
            var categories = from category in context.Category
                             select category.CategoryName;

            Categories = await categories.ToListAsync();

            var articles = from article in context.Article
                           where article.IssueID == id
                           select article;

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
        public async Task<IActionResult> Create([FromRoute]int? id, [Bind("Language,Category,Title,Subtitle,Writer,Picture,Text")] Article article)
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

                // Assign value to the MemberID that reefers to the writer of the article
                var writerID = context.Member.First(m => m.Name == article.Writer).ID;
                article.MemberID = writerID;

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
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Language,Title,Subtitle,Picture,Text,DateTime,Rate,NumberOfVotes,NumberOfFavorites")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(article);
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
                return RedirectToAction(nameof(Index));
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

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await context.Article.FindAsync(id);
            context.Article.Remove(article);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return context.Article.Any(e => e.Id == id);
        }
    }
}
