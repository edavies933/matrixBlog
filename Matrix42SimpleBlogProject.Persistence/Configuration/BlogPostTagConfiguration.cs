using Matrix42SimpleBlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matrix42SimpleBlogProject.Persistence.Configuration;

public class BlogPostTagConfiguration : IEntityTypeConfiguration<BlogPostTag>
{
    public void Configure(EntityTypeBuilder<BlogPostTag> builder)
    {
        builder.ToTable("blog_post_tag");
        builder.HasKey(m => m.Id);
        builder.Property(a => a.Id).IsRequired();
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.BlogPostId).HasColumnName("blog_post_id");
        builder.Property(a => a.TagId).HasColumnName("tag_id");
    }
}