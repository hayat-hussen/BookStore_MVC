using BookStore.DataAccess.Data; // Importing the data context for database operations
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Importing ASP.NET Core MVC

namespace bookStoreWeb.Areas.Admin.Controllers // Defining the namespace for the controller
{
    [Area("Admin")]
    public class CategoryController : Controller // Creating the CategoryController class
    {
        private readonly IUnitOfWork _unitOfWork; // Declaring a private variable for the database context

        // Constructor to initialize the database context
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; // Assigning the passed database context to the private variable
        }

        // Action method to display the list of categories
        //public IActionResult Index()
        //{
        //    List<Category> objCategoryList = _db.Categories.ToList(); // Getting the list of categories from the database
        //    return View(objCategoryList); // Returning the list to the view
        //}
        public IActionResult Index()
        {
            var categories = _unitOfWork.Category.GetAll().ToList(); // Ensure this is a list

            return View(categories); // Pass the collection to the view
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
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Cant Be Exacly The Same With The Name");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj); // Adding the new category to the database
                _unitOfWork.Save(); // Saving the changes to the database
                TempData["success"] = "category created successfully";


                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb); // Pass the category to the view for editing
        }

        // Action method to handle the form submission for editing a category
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                // Check if the name and display order are the same
                if (obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("Name", "The Display Order cannot be the same as the Name.");
                }
                else
                {
                    // Update the category
                    _unitOfWork.Category.Update(obj); // Updating the category in the database
                    _unitOfWork.Save(); // Saving the changes to the database
                    TempData["success"] = "Category edited successfully";

                    return RedirectToAction("Index", "Category"); // Redirect to the index action
                }
            }

            // If we reach here, it means ModelState is invalid or the name/display order check failed
            return View(obj); // Return the category object to the view for corrections
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb); // Pass the category to the view for confirmation
        }

        // Action method to handle the form submission for deleting a category
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj); // Remove the object from the context
            _unitOfWork.Save(); // Save changes to the database
            TempData["success"] = "category deleted successfully";


            return RedirectToAction("Index", "Category"); // Redirect after deletion
        }


    }
}