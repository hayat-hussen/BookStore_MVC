using bookStoreWeb.Data; // Importing the data context for database operations
using bookStoreWeb.Models; // Importing the model classes
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC

namespace bookStoreWeb.Controllers // Defining the namespace for the controller
{
    public class CategoryController : Controller // Creating the CategoryController class
    {
        private readonly ApplicationDbContext _db; // Declaring a private variable for the database context

        // Constructor to initialize the database context
        public CategoryController(ApplicationDbContext db)
        {
            _db = db; // Assigning the passed database context to the private variable
        }

        // Action method to display the list of categories
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList(); // Getting the list of categories from the database
            return View(objCategoryList); // Returning the list to the view
        }

        // Action method to show the create category form
        public IActionResult Create()
        {
            return View(); // Returning the view for creating a new category
        }

        // Action method to handle the form submission for creating a category
        [HttpPost] // This method will respond to POST requests
        public IActionResult Create(Category obj)
        {
            if(obj.Name== obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name","The Display Cant Be Exacly The Same With The Name");
            }

            if (ModelState.IsValid) 
            { 
            _db.Categories.Add(obj); // Adding the new category to the database
            _db.SaveChanges(); // Saving the changes to the database
                              
            return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}