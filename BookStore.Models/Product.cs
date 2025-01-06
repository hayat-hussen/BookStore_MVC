using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Product
    {

        [Key] // Primary key
        public int Id { get; set; }
        [Required] // Must be provided
        public string Title { get; set; }
        public string Description { get; set; }
        [Required] // Must be provided
        public string ISBN { get; set; }
        [Required] // Must be provided
        public string Author { get; set; }
        [Required] // Must be provided
        [Display(Name ="Price")]
        [Range(1,1000)]
        public double ListPrice { get; set; }

        [Required] // Must be provided
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }
        [Required] // Must be provided
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }
        [Required] // Must be provided
        [Display(Name = "Price for 100 +")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

    }
}
