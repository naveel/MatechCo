using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Matechco.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Display(Name = "Product Code")]
        [Required(ErrorMessage = "This field is required")]
        public string Code { get; set; }
        public string Action { get; set; }
        [Display(Name = "Product Type")]
        public int? ProductTypeId { get; set; }
    }
}