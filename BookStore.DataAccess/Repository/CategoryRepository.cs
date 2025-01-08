using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    // CategoryRepository class that inherits from the generic Repository class and implements ICategoryRepository
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db; // Instance of ApplicationDbContext for database operations

        // Constructor that initializes the CategoryRepository with the ApplicationDbContext
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; // Assign the provided ApplicationDbContext to _db
        }

        

        // Method to save changes to the database


        // Method to update an existing category
        public void Update(Category obj)
        {
            _db.Categories.Update(obj); // Updates the specified category in the database
        }
    }
}