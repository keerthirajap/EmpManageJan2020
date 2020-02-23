namespace CompName.ManageStocks.WebAppMVC.Areas.Manufacturer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddManufacturerViewModel
    {
        public long ManufacturerId { get; set; }

        [Required(ErrorMessage = "The Manufacturer Name field is required.")]
        [StringLength(30, ErrorMessage = "The Manufacturer Name must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Manufacturer Name *")]
        public string ManufacturerName { get; set; }

        [Required(ErrorMessage = "The Manufacturer Email Id field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Id")]
        [Display(Name = "Manufacturer Email Id *")]
        public string ManufacturerEmailId { get; set; }

        [Required(ErrorMessage = "The Manufacturer WebSite field is required.")]
        [StringLength(30, ErrorMessage = "The Manufacturer WebSite must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Manufacturer WebSite *")]
        public string ManufacturerWebSite { get; set; }

        [Required(ErrorMessage = "The Manufacturer Address1 field is required.")]
        [StringLength(30, ErrorMessage = "The Manufacturer Address1 must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Manufacturer Address1 *")]
        public string ManufacturerAddress1 { get; set; }

        [StringLength(30, ErrorMessage = "The Manufacturer Address2 must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Manufacturer Address2 *")]
        public string ManufacturerAddress2 { get; set; }

        public long ManufacturerCountryId { get; set; }

        public long ManufacturerCityId { get; set; }

        public long ManufacturerStateId { get; set; }

        public string ManufacturerZipCode { get; set; }
    }
}