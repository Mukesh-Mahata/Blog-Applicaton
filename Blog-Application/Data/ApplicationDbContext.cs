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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Technology" },
                new Category { Id = 2, Name = "Lifestyle" },
                new Category { Id = 3, Name = "Travel" },
                new Category { Id = 4, Name = "Food" }
            );

            modelBuilder.Entity<Models.Post>().HasData(
                 new Post
                 {
                     Id = 1,
                     Title = "Tech Post 1",
                     Content = "Content of Tech Post 1",
                     Author = "John Doe",
                     PublishedDate = new DateTime(2023, 1, 1),
                     CategoryId = 1,
                     FeatureImagePath = "tech_image.jpg", 
                 },
                new Post
                {
                    Id = 2,
                    Title = "Health Post 1",
                    Content = "Content of Health Post 1",
                    Author = "Jane Doe",
                    PublishedDate = new DateTime(2023, 1, 1), 
                    CategoryId = 2,
                    FeatureImagePath = "health_image.jpg", 
                },
                new Post
                {
                    Id = 3,
                    Title = "Lifestyle Post 1",
                    Content = "Content of Lifestyle Post 1",
                    Author = "Alex Smith",
                    PublishedDate = new DateTime(2023, 1, 1), 
                    CategoryId = 3,
                    FeatureImagePath = "lifestyle_image.jpg", 
                }
            );
    
        }   
    }
}

