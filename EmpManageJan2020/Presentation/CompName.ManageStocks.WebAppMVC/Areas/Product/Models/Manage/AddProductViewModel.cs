namespace CompName.ManageStocks.WebAppMVC.Areas.Product.Models.Manage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddProductViewModel
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Product Price")]
        public decimal? ProductPrice { get; set; }
    }
}