using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore.Models
{
    public class Product
    {

        [Key] // Primary key
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "List Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "List Price must be a positive number")]
        public decimal ListPrice { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal Price { get; set; }

        public decimal Price50 { get; set; }
        public decimal Price100 { get; set; }
        // Foreign key property
        public int CategoryId { get; set; } // Foreign key property
        [ForeignKey("CategoryId")]
        [ValidateNever]

        public Category Category { get; set; } // Navigation property
        [ValidateNever]
        [Required(ErrorMessage = "Image  is required")]

        public string ImageUrl { get; set; } 





    }
}
