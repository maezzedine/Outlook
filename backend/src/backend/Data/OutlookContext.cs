using backend.Areas.Identity;
using backend.Models;
using backend.Models.Relations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class OutlookContext : IdentityDbContext<OutlookUser>
    {
        public OutlookContext(DbContextOptions<OutlookContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Article>()
                .HasOne(a => a.Issue)
                .WithMany(c => c.Articles)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Article>()
                .HasOne(a => a.Member)
                .WithMany(m => m.Articles)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Article>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Article)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFavoritedArticleRelation>()
               .HasOne(uf => uf.Article)
               .WithMany(a => a.Favorites)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFavoritedArticleRelation>()
               .HasOne(uf => uf.User)
               .WithMany(u => u.Favorites)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserRateArticle>()
               .HasOne(ur => ur.Article)
               .WithMany(a => a.Rates)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserRateArticle>()
               .HasOne(uf => uf.User)
               .WithMany(u => u.Rates)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Category>()
                .HasMany(c => c.JuniorEditors)
                .WithOne(e => e.Category);

            builder.Entity<Issue>()
                .HasAlternateKey(i => new { i.VolumeID, i.IssueNumber });

            builder.Entity<Volume>()
                .HasAlternateKey(v => v.VolumeNumber);

            builder.Entity<Category>()
                .HasAlternateKey(c => c.CategoryName);

            builder.Entity<Member>()
                .HasAlternateKey(m => m.Name);
        }

        public DbSet<Volume> Volume { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Issue> Issue { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<UserFavoritedArticleRelation> UserFavoritedArticleRelation { get; set; }
        public DbSet<UserRateArticle> UserRateArticle { get; set; }

    }
}
