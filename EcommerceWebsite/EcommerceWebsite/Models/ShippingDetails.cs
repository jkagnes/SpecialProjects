using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWebsite.Models
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name  is required")]
        [Display(Name = "Last name:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Street address is required")]
        [Display(Name = "Address 1:")]
        public string Line1 { get; set; }

        [Display(Name = "Address 2:")]
        public string Line2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City:")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [Display(Name = "State:")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country:")]
        public string Country { get; set; }

        [Display(Name = "Zip:")]
        public string Zip { get; set; }
    }
}