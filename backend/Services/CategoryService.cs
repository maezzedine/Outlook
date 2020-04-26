using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class CategoryService
    {
        public static async Task GetCategoryDetails(Category category, int issueId, OutlookContext context)
        {
            category.ArticlesCount = context.Article.Where(a => (a.CategoryID == category.Id) && (a.IssueID == issueId)).Count();
            await GetCategoryJuniorEditors(category, context);
        }

        public static async Task GetCategoryJuniorEditors(Category category, OutlookContext context)
        {
            category.TagName = category.Tag.ToString();
            
            var juniorEditorsIDs = from categoryEditor in context.CategoryEditor
                                   where categoryEditor.CategoryID == category.Id
                                   select categoryEditor.MemberID;

            var juniorEditors = from member in context.Member
                                where juniorEditorsIDs.Contains(member.ID)
                                select member;

            category.JuniorEditors = await juniorEditors.ToListAsync();
        }
    }
}
