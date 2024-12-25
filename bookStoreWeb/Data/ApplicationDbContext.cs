using bookStoreWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace bookStoreWeb.Data
{
    // Represents the application's database context, inheriting from DbContext.
    public class ApplicationDbContext : DbContext
    {
        // Constructor for ApplicationDbContext, accepting options for configuration.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) // Passes the options to the base DbContext class.
        {
            // Additional initialization can be done here if needed.
        }

        // Here, you would typically define DbSet properties for your entities.
        // For example:
        // public DbSet<Book> Books { get; set; }

        // Represents a collection of Category entities, corresponding to the Categories table in the database.
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial data for the Categories table
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "action", DisplayOrder = 1 }, // First category: Action
                new Category { Id = 2, Name = "scifi", DisplayOrder = 2 },  // Second category: Science Fiction
                new Category { Id = 3, Name = "history", DisplayOrder = 3 } // Third category: History
            );
        }
    }
}