using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repository
{
    // Generic repository class implementing IRepository interface
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db; // ApplicationDbContext instance for database operations
        internal DbSet<T> dbSet; // DbSet for the entity type T

        // Constructor that initializes the repository with the ApplicationDbContext
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>(); // Set the DbSet for the entity type
        }

        // Method to add a new entity to the database
        public void Add(T entity)
        {
            dbSet.Add(entity); // Adds the entity to the DbSet
        }

        // Method to retrieve a single entity based on a filter
        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet; // Create a queryable from the DbSet
            query = query.Where(filter); // Apply the filter
            return query.FirstOrDefault(); // Return the first matching entity or null
        }

        // Method to retrieve all entities from the database
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet; // Create a queryable from the DbSet
            return query.ToList(); // Return all entities as a list
        }

        // Method to get the first entity or default (not yet implemented)
       

        // Method to remove an entity from the database
        public void Remove(T entity)
        {
            dbSet.Remove(entity); // Removes the specified entity from the DbSet
        }

        // Method to remove a range of entities from the database
        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity); // Removes a collection of entities from the DbSet
        }
    }
}