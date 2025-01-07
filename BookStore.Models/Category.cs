using System.ComponentModel; // For DisplayName attribute
using System.ComponentModel.DataAnnotations; // For validation attributes

namespace BookStore.Models // Namespace for the model
{
    public class Category // Category class
    {
        [Key] // Primary key
        public int Id { get; set; }

        [Required] // Must be provided
        [DisplayName("Category Name")] // Display name
        [MaxLength(30)] // Max length of 30 characters
        public string Name { get; set; }

        [DisplayName("Display Order")] // Display name
        [Range(1, 100)] // Value must be between 1 and 100
        public int DisplayOrder { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}