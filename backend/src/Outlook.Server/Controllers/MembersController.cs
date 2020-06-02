using Outlook.Server.Data;
using Outlook.Server.Models;
using Outlook.Server.Models.Interfaces;
using Outlook.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Outlook.Server.Controllers
{
    [Authorize(Roles = "Admin, Editor-In-Chief", AuthenticationSchemes = "Identity.Application")]
    public class MembersController : Controller
    {
        private readonly OutlookContext context;
        private readonly MemberService memberService;
        private readonly Logger.Logger logger;
        public static List<string> Categories;

        public MembersController(
            OutlookContext context,
            MemberService memberService)
        {
            this.context = context;
            this.memberService = memberService;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.server);

            // Save the List of categories names to be accessed
            Categories = context.Category.Select(c => c.CategoryName).ToList();
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            var members = context.Member
                .Include(m => m.Category)
                .Where(m => (m.Position != Position.كاتب_صحفي) && (m.Position != Position.Staff_Writer));

            return View(await members.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await context.Member
                .Include(m => m.Category)
                .Include(m => m.Articles)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Position,Category")] Member member)
        {
            ModelState.Remove("Category.CategoryName");
            if (ModelState.IsValid)
            {
                var memberExists = await context.Member.FirstOrDefaultAsync(m => m.Name == member.Name);

                if (memberExists != null)
                {
                    return ValidationProblem(detail: "Member name already exists.");
                }

                member.Category = (memberService.IsJuniorEditor(member)) ? context.Category
                        .First(c => c.CategoryName == member.Category.CategoryName) : null;

                context.Add(member);
                await context.SaveChangesAsync();

                logger.Log($"{HttpContext.User.Identity.Name} created member `{member.Name}` with position {member.Position} " +
                    $"(Category = {member.Category.CategoryName}) ");

                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await context.Member.Include(m => m.Category).FirstOrDefaultAsync(m => m.ID == id);

            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Position,Category")] Member member)
        {
            if (id != member.ID)
            {
                return NotFound();
            }

            ModelState.Remove("Category.CategoryName");
            if (ModelState.IsValid)
            {
                try
                {
                    var originalMember = await context.Member
                        .Include(m => m.Category)
                        .FirstOrDefaultAsync(m => m.ID == member.ID);


                    if (memberService.IsJuniorEditor(originalMember))
                    {
                        logger.Log($"{HttpContext.User.Identity.Name} editted member {FormatLogEditMessage(originalMember)}");

                        if ((member.Position != Position.Junior_Editor) && (member.Position != Position.رئيس_قسم))
                        {
                            originalMember.Category = null;
                        }
                        else
                        {
                            var newCategory = await context.Category
                                .FirstOrDefaultAsync(c => c.CategoryName == member.Category.CategoryName);

                            if (newCategory != null && newCategory != originalMember.Category)
                            {
                                originalMember.Category = newCategory;
                            }
                        }
                    }
                    else
                    {
                        if (memberService.IsJuniorEditor(member))
                        {
                            var newCategory = await context.Category
                                .FirstOrDefaultAsync(c => c.CategoryName == member.Category.CategoryName);

                            if (newCategory != null)
                            {
                                originalMember.Category = newCategory;
                            }
                        }
                    }

                    originalMember.Position = member.Position;
                    await context.SaveChangesAsync();

                    logger.Log($"to become:{FormatLogEditMessage(originalMember)}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.ID))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await context.Member
                .Include(m => m.Category)
                .Include(m => m.Articles)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var member = await context.Member.FindAsync(id);

            logger.Log($"{HttpContext.User.Identity.Name} admits to delete member `{member.Name}`.");
            context.Member.Remove(member);
            await context.SaveChangesAsync();
            logger.Log($"Delete Completed.");

            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return context.Member.Any(e => e.ID == id);
        }

        private string FormatLogEditMessage(Member member)
        {
            return $@"Name: { member.Name}
                      Position: {member.Position}
                      Category: {member.Category?.CategoryName}";
        }
    }
}
