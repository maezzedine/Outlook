using backend.Data;
using backend.Models;
using backend.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace backend.Services
{
    public class ArticleService
    {
        public static async Task GetArticleProperties(Article article, OutlookContext context)
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

        public static void GetArticleWriterAndCategory(Article article, OutlookContext context)
        {
            var writer = context.Member.First(m => m.ID == article.MemberID);
            var category = context.Category.First(c => c.Id == article.CategoryID);
        }

        public static async Task EditArticleWriter(Article article, OutlookContext context)
        {
            Member writer;
            if (article.Member.Name != "New Writer")
            {
                writer = context.Member.First(m => m.Name == article.Member.Name);
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
            article.MemberID = writer.ID;
        }

        public static async Task AddArticlePicture(Article article, IFormFile picture, OutlookContext context, string webRootPath)
        {
            // Add unique name to avoid possible name conflicts
            var uniqueImageName = DateTime.Now.Ticks.ToString() + ".jpg";
            var articleImageFolderPath = Path.Combine(new string[] { webRootPath, "img", "Articles\\" });
            var articleImageFilePath = Path.Combine(articleImageFolderPath, uniqueImageName);
            if (!Directory.Exists(articleImageFolderPath))
            {
                Directory.CreateDirectory(articleImageFolderPath);
            }
            using (var fileStream = new FileStream(articleImageFilePath, FileMode.Create, FileAccess.Write))
            {
                // Copy the photo to storage
                await picture.CopyToAsync(fileStream);
            }
            // Save picture local path in the article object
            article.PicturePath = @"/img/Articles/" + uniqueImageName;
        }

        public static void DeleteArticlePicture(Article article, string webRootPath)
        {
            var path = webRootPath + article.PicturePath;
            System.IO.File.Delete(path);
            article.PicturePath = null;
        }

        public static void UpdateArticleInfo(Article article, Language lang, int categoryID, string title, string subtitle, string text)
        {
            article.Language = lang;
            article.CategoryID = categoryID;
            article.Title = title;
            article.Subtitle = subtitle;
            article.Text = text;
        }
    }
}
