using Matrix42SimpleBlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matrix42SimpleBlogProject.Persistence.Configuration;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("blogs");
        builder.HasKey(m => m.Id);
        builder.Property(a => a.Id).IsRequired();
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.Name).HasColumnName("name"); 
        builder.Property(a => a.Description).HasColumnName("description");
        builder.Property(a => a.CreatedDate).HasColumnName("created_date");
        builder.Property(a => a.LastModifiedDate).HasColumnName("last_modified_date");
        builder.Property(a => a.UserId).HasColumnName("user_id");
    }
}