using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class CategoryService
    {
        private readonly OutlookContext context;

        public CategoryService(OutlookContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// GetCategoryDetails is a method that gets a category's tag, junior editors and its number of articles in a certain issue
        /// </summary>
        /// <param name="category"></param>
        /// <param name="issueId"></param>
        public async Task GetCategoryDetails(Category category, int issueId)
        {
            category.ArticlesCount = context.Article.Where(a => (a.CategoryID == category.Id) && (a.IssueID == issueId)).Count();
            await GetCategoryJuniorEditors(category);
        }

        /// <summary>
        /// GetCategoryJuniorEditors is a method that gets a category's tag and junior editors
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task GetCategoryJuniorEditors(Category category)
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
