using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Models.Relations;

namespace backend.Data
{
    public class OutlookContext : DbContext
    {
        public OutlookContext (DbContextOptions<OutlookContext> options)
            : base(options)
        {
        }

        public DbSet<Volume> Volume { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Issue> Issue { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Reply> Reply { get; set; }
        public DbSet<ArabicBoardRelation> ArabicBoard { get; set; }
        public DbSet<EnglishBoardRelation> EnglishBoard { get; set; }
        public DbSet<CategoryEditorRelation> CategoryEditor { get; set; }
        public DbSet<MemberFavoritedArticleRelation> MemberFavoritedArticle { get; set; }

    }
}
