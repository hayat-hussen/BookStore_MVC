using System.ComponentModel.DataAnnotations;

namespace bookStoreWeb.Models
{
    public class Category
    {// prop  category class
        // to make id primary [key],or Category__
        [Key]
        public int  Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
