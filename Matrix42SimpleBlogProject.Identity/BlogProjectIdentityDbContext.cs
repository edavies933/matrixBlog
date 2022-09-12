using Matrix42SimpleBlogProject.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Matrix42SimpleBlogProject.Identity;

public class BlogProjectIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public BlogProjectIdentityDbContext(DbContextOptions<BlogProjectIdentityDbContext> options) : base(options)
    {
    }
}

