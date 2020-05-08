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
        private readonly OutlookContext context;

        public ArticleService(OutlookContext context)
        {
            this.context = context;
        }

        /// <summary> 
        /// GetArticleProperties is a method that provides an article with its properties including object from one-to-many and many-to-many relations 
        /// </summary>
        /// <param name="article"></param>
        public async Task GetArticleProperties(Article article)
        {
            // Add the category
            var category = await context.Category.FindAsync(article.CategoryID);
            article.CategoryTagName = category.Tag.ToString();

            // Add the writer
            var writer = await context.Member.FindAsync(article.MemberID);
            writer.Articles = null;

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
                comment.Article = null;
                comment.User = owner;
            }

            article.Comments = await comments.ToListAsync();

            article.Issue = null;
            article.Rates = null;
            article.Favorites = null;
            article.Category.Articles = null;
        }

        /// <summary>
        /// GetArticleWriterAndCategory is a method that provides an article with its writer and category objects
        /// </summary>
        /// <param name="article"></param>
        public void GetArticleWriterAndCategory(Article article)
        {
            var writer = context.Member.First(m => m.ID == article.MemberID);
            var category = context.Category.First(c => c.Id == article.CategoryID);
        }

        /// <summary>
        /// EditArticleWriter is a method that adds or changes an article's writer 
        /// </summary>
        /// <param name="article"></param>
        public async Task EditArticleWriter(Article article)
        {
            Member writer;
            if (article.Member.Name != "+ NEW WRITER")
            {
                writer = context.Member.First(m => m.Name == article.Member.Name);
            }
            else
            {
                // Create a new writer if needed
                writer = new Member { Name = article.NewWriter };

                // Decide whether the writer writes for the English section or the Arabic section
                if (Regex.IsMatch(article.NewWriter, @"^[a-zA-Z.\-\s]*$"))
                {
                    writer.Position = Position.Staff_Writer;
                }
                else
                {
                    writer.Position = Position.كاتب_صحفي;
                }
                context.Member.Add(writer);
            }
            article.Member = writer;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// AddArticlePicture is a method that adds or changes an article's picture
        /// </summary>
        /// <param name="article"></param>
        /// <param name="picture"></param>
        /// <param name="webRootPath">the root path of the project</param>
        public async Task AddArticlePicture(Article article, IFormFile picture, string webRootPath)
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

        /// <summary>
        /// DeleteArticlePicture is a method that deletes an article's picture
        /// </summary>
        /// <param name="article"></param>
        /// <param name="webRootPath">the root path of the project</param>
        public void DeleteArticlePicture(Article article, string webRootPath)
        {
            var path = webRootPath + article.PicturePath;
            System.IO.File.Delete(path);
            article.PicturePath = null;
        }

        /// <summary>
        /// UpdateArticleInfo is a method that updates the old values of an article with the given parameters
        /// </summary>
        /// <param name="article"></param>
        /// <param name="lang"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="text"></param>
        public void UpdateArticleInfo(Article article, Language lang, string title, string subtitle, string text)
        {
            article.Language = lang;
            article.Title = title;
            article.Subtitle = subtitle;
            article.Text = text;
        }
    }
}
