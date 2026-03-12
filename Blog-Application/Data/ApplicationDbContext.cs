using Blog_Application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog_Application.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
      public DbSet<Category> Categories { get; set; }

      public DbSet<Models.Post> Posts { get; set; }

      public DbSet<Models.Comment> Comments { get; set; }
    }
}
