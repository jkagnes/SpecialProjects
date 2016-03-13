using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace EcommerceWebsite.Controllers
{
    public class ProductController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();
        //
        // GET: /Product/
        public ActionResult MainProducts(string category, string searchCriteria)
        {
            var _category = db.Categories.Where(x => x.Name == category).FirstOrDefault();

            var products = (from p in db.Products.Include("Product_Category_Mapping")
                                        where p.Product_Category_Mapping
                                                    .Any(c => category == null || 
                                                            c.Category.Name == category || 
                                                            c.Category.ParentCategoryId == _category.Id)
                                        select p).ToList();

            return View("List", products);
            //return View(products);
            //return View("MainProducts", "~/Views/Shared/_MenuLayout.cshtml", products);
        }

        public ActionResult Search(string searchCriteria)
        {
            var products = (from p in db.Products
                            where (p.Name.Contains(searchCriteria) ||  p.FullDescription.Contains(searchCriteria))
                            select p).ToList();

            return View("List", products);
        }

        public ActionResult SubcategoryProducts(string category)
        {
            var products = (from p in db.Products.Include("Product_Category_Mapping")
                            where p.Product_Category_Mapping
                                        .Any(c => category == null || c.Category.Name == category)
                            select p).ToList();

            return View("List", products);
            //return View(products);
            //return View("SubcategoryProducts", "~/Views/Shared/_MenuLayout.cshtml", products);
        }

        public ActionResult Detail(int id) {
            var product = db.Products.Where(p => p.Id == id).FirstOrDefault();
            return View(product);            
            //return View("Detail", "~/Views/Shared/_Layout.cshtml", product);
        }
	}
}