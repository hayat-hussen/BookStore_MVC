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
        private readonly IWebHostEnvironment _webHostEnvironment;
        // Constructor to initialize the database context
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork; // Assigning the passed database context to the private variable
            _webHostEnvironment = webHostEnvironment;
        }

        // Action method to display the list of products
        //public IActionResult Index()
        //{
        //    List<Product> objProductList = _unitOfWork.Product.GetAll().ToList(); // Getting the list of products from the database
        //    return View(objProductList); // Returning the list to the view
        //}
        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList(); // Ensure this is a list

            return View(products); // Pass the collection to the view
        }

        // Action method to show the create Product form
        public IActionResult Upsert(int? id)
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
            //create function
            if (id == null || id == 0)
            {
                return View(productVM);

            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);

            }
            //update

            // Returning the view for creating a new Product
        }

        // Action method to handle the form submission for creating a Product
        [HttpPost] // This method will respond to POST requests
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {


            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string ProductPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(ProductPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product); // Adding the new Product to the database

                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product); // Adding the new Product to the database

                }
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
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Invalid ID" });
            }

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            _unitOfWork.Product.Remove(productFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Product deleted successfully" });
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

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            // Fetch all products with their associated categories
            var products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            // Map the products to a DTO or a specific structure
            var productList = products.Select(p => new
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ISBN = p.ISBN,
                Author = p.Author,
                ListPrice = p.ListPrice,
                Price = p.Price,
                Price50 = p.Price50,
                Price100 = p.Price100,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : null, // Safely access Category
                CategoryDisplayOrder = p.Category != null ? p.Category.DisplayOrder : (int?)null, // Optional display order
                ImageUrl = p.ImageUrl // Assuming ImageUrl is a property of Product
            }).ToList();

            // Return the data in the desired format
            return Json(new { data = productList });
        }
        #endregion






    }
}