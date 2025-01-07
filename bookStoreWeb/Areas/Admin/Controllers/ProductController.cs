using BookStore.DataAccess.Data; // Importing the data context for database operations
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // Importing ASP.NET Core MVC

namespace bookStoreWeb.Areas.Admin.Controllers // Defining the namespace for the controller
{
    [Area("Admin")]
    public class ProductController : Controller // Creating the ProductController class
    {
        private readonly IUnitOfWork _unitOfWork; // Declaring a private variable for the database context

        // Constructor to initialize the database context
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; // Assigning the passed database context to the private variable
        }

        // Action method to display the list of products
        //public IActionResult Index()
        //{
        //    List<Product> objProductList = _unitOfWork.Product.GetAll().ToList(); // Getting the list of products from the database
        //    return View(objProductList); // Returning the list to the view
        //}
        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll().ToList(); // Ensure this is a list

            return View(products); // Pass the collection to the view
        }

        // Action method to show the create Product form
        public IActionResult Create()
        {
            // ViewBag.CategoryList = CategoryList;

            //  ViewData["CategoryList"] = CategoryList;
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()

                }
                ),
                Product = new Product()
            };
            return View(productVM); // Returning the view for creating a new Product
        }

        // Action method to handle the form submission for creating a Product
        [HttpPost] // This method will respond to POST requests
        public IActionResult Create(ProductVM productVM)
        {
            

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product); // Adding the new Product to the database
                _unitOfWork.Save(); // Saving the changes to the database
                TempData["success"] = "Product created successfully";


                return RedirectToAction("Index", "Product");
            }
            else
            {

                productVM.CategoryList = _unitOfWork.Category
                             .GetAll().Select(u => new SelectListItem
                             {
                                 Text = u.Name,
                                 Value = u.Id.ToString()

                             }
                             );
                return View(productVM);
            }
            
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb); // Pass the Product to the view for editing
        }

        // Action method to handle the form submission for editing a Product
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {

                // Update the Product
                _unitOfWork.Product.Update(obj); // Updating the Product in the database
                _unitOfWork.Save(); // Saving the changes to the database
                    TempData["success"] = "Product edited successfully";

                    return RedirectToAction("Index", "Product"); // Redirect to the index action
                
            }

            // If we reach here, it means ModelState is invalid or the name/display order check failed
            return View(obj); // Return the product object to the view for corrections
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb); // Pass the product to the view for confirmation
        }

        // Action method to handle the form submission for deleting a Product
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj); // Remove the object from the context
            _unitOfWork.Save(); // Save changes to the database
            TempData["success"] = "Product deleted successfully";


            return RedirectToAction("Index", "Product"); // Redirect after deletion
        }


      
        


    }
}