using EcommerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceWebsite.Controllers
{
    public class NavController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();
        //
        // GET: /Nav/
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            var categoriesList = new List<CategoryViewModel>();
            foreach (var item in db.Categories)
            {
                if (item.ParentCategoryId == 0)
                {
                    var subCategories = db.Categories
                                                    .Where(x => x.ParentCategoryId == item.Id)
                                                    .Select(x => x.Name);
                    categoriesList.Add(new CategoryViewModel { Name = item.Name, SubCategory = subCategories.ToList() });
                }
            }            
            return PartialView(categoriesList);
        }

        public PartialViewResult TopMenu()
        {
            var categoriesList = new List<CategoryViewModel>();
            foreach (var item in db.Categories)
            {
                if (item.ParentCategoryId == 0)
                {
                    var subCategories = db.Categories
                                                    .Where(x => x.ParentCategoryId == item.Id)
                                                    .Select(x => x.Name);
                    categoriesList.Add(new CategoryViewModel { Name = item.Name, SubCategory = subCategories.ToList() });
                }
            }
            return PartialView(categoriesList);
        }
	}
}