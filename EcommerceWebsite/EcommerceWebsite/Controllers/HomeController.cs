using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace EcommerceWebsite.Controllers
{
    public class HomeController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();
        //
        // GET: /Home/
        /*public ActionResult Index()
        {
            EcommerceEntities db = new EcommerceEntities();
            IEnumerable<Product> products = db.Products;
            return View(products);
        }*/

        public ActionResult Home()
        {
            return View();
        }


        public ActionResult Index(string category)
        {            
            var products = (from p in db.Products.Include("Product_Category_Mapping")
                            where p.Product_Category_Mapping.Any(c => category == null || c.Category.Name == category)
                            select p).ToList();
                                         
            return View(products);
        }
	}
}