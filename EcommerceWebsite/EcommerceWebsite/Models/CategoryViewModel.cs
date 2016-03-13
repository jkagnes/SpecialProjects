using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceWebsite.Models
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public List<string> SubCategory { get; set; }

        public CategoryViewModel() { SubCategory = new List<string>(); }
    }
}