using Matrix42SimpleBlogProject.Domain.Common;
using Matrix42SimpleBlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Matrix42SimpleBlogProject.Persistence
{
    public class BlogProjectDbContext : DbContext
    {

        public BlogProjectDbContext(DbContextOptions<BlogProjectDbContext> options) : base(options)
        {

        }

        public DbSet<Blog>? Blogs { get; set; }
        public DbSet<BlogPost>? BlogPosts { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Tag>? Tags { get; set; }
        public DbSet<BlogPostTag>? BlogPostTag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogProjectDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}