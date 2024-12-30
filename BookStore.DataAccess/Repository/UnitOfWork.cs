using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db; // Instance of ApplicationDbContext for database operations
        public ICategoryRepository Category { get; private set; }

        // Constructor that initializes the CategoryRepository with the ApplicationDbContext
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db; // Assign the provided ApplicationDbContext to _db
            Category = new CategoryRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges(); // Persists all changes made in this context to the database
        }
    }
}
