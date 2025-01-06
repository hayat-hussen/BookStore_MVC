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
    // ProductRepository class that inherits from the generic Repository class and implements IProductRepository
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db; // Instance of ApplicationDbContext for database operations

        // Constructor that initializes the ProductRepository with the ApplicationDbContext
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; // Assign the provided ApplicationDbContext to _db
        }

        // Method to save changes to the database


        // Method to update an existing Product
        public void Update(Product obj)
        {
            _db.Products.Update(obj); // Updates the specified Product in the database
        }
    }
}