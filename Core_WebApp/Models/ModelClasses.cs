using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Core_WebApp.Models
{
  public class Category
    {
        [Key]
        public int CategoryRowID { get; set; }
        
        [Required(ErrorMessage ="Category id is mandatory")]        
        public  string CateogryID { get; set; }
        
        [Required(ErrorMessage = "Category name is mandatory")]
        public string CategoryName { get; set; }
        
        [Required(ErrorMessage = "Base price is mandatory")] 
        public int BasePrice { get; set; }

        //One to many relationship
        public ICollection<Product> Products { get; set; }
    }//End of Category class

    public class Product
    {
        [Key]
        public int ProductRowID { get; set; }

        [Required(ErrorMessage = "Product id is mandatory")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Product name is mandatory")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Manufacturer  is mandatory")]
        public string  Manufacturer { get; set; }

        [Required(ErrorMessage = "Description  is mandatory")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category row id  is mandatory")]
        public int CaegoryRowID { get; set; }

        //foregin key
        public Category Category { get; set; }

    }//End of Product class


}
