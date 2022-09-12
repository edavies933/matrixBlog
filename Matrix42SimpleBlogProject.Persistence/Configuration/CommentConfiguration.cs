using Matrix42SimpleBlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matrix42SimpleBlogProject.Persistence.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments");
        builder.HasKey(m => m.Id);
        builder.Property(a => a.Id).IsRequired();
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.BlogPostId).HasColumnName("blog_post_id");
        builder.Property(a => a.UserId).HasColumnName("user_id");
        builder.Property(a => a.Content).HasColumnName("content");
        builder.Property(a => a.CreatedDate).HasColumnName("created_date");
        builder.Property(a => a.LastModifiedDate).HasColumnName("last_modified_date");
    }
}