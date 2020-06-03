using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Outlook.Models.Core.Models;
using Outlook.Models.Core.Relations;

namespace Outlook.Models.Data
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
                .HasOne(a => a.Writer)
                .WithMany(m => m.Articles)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Article>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Article)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFavoriteArticle>()
               .HasOne(uf => uf.Article)
               .WithMany(a => a.Favorites)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFavoriteArticle>()
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
                .HasMany(c => c.Editors)
                .WithOne(e => e.Category);

            //builder.Entity<Issue>()
            //    .HasAlternateKey(i => new { i.Volume.Id, i.Number });

            builder.Entity<Volume>()
                .HasAlternateKey(v => v.Number);

            builder.Entity<Category>()
                .HasAlternateKey(c => c.Name);

            builder.Entity<Member>()
                .HasAlternateKey(m => m.Name);
        }

        public DbSet<Volume> Volume { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Issue> Issue { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<UserFavoriteArticle> UserFavoriteArticle { get; set; }
        public DbSet<UserRateArticle> UserRateArticle { get; set; }
    }
}
