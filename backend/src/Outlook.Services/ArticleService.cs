using Microsoft.AspNetCore.Http;
using Outlook.Models.Core.Models;
using Outlook.Models.Data;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Outlook.Models.Services.OutlookConstants;

namespace Outlook.Services
{
    public class ArticleService
    {
        private readonly OutlookContext context;

        public ArticleService(OutlookContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// EditArticleWriter is a method that adds or changes an article's writer 
        /// </summary>
        /// <param name="article"></param>
        /// <param name="writerName"></param>
        public Article SetArticleWriter(Article article, string writerName)
        {
            Member writer;
            writer = context.Member
                   .FirstOrDefault(m => m.Name == writerName);

            if (writer == null)
            {
                writer = new Member { Name = writerName };

                // Declare writer's position based on their name
                writer.Position = Regex.IsMatch(writerName, @"^[a-zA-Z.\-+\s]*$") ? Position.Staff_Writer : Position.كاتب_صحفي;
                context.Member.Add(writer);
            }
            article.Writer = writer;
            return article;
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
    }
}
