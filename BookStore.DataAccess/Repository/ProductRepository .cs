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

        public Product? Get(Func<Product, bool> value)
        {
            throw new NotImplementedException();
        }

        // Method to save changes to the database


        // Method to update an existing Product
        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id); // Updates the specified Product in the database
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Description = obj.Description;
                objFromDb.Author = obj.Author;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.CategoryId = obj.CategoryId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
               

            }
        }
    }
}