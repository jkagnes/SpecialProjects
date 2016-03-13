using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceWebsite.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();
        //
        // GET: /Admin/
        public ViewResult AdminIndex()
        {            
            IEnumerable<Product> products = db.Products;
            return View(products);
        }

        public ViewResult Edit(int Id)
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == Id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {/*
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }*/
                SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("AdminIndex");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                deletedProduct.Name);
            }
            return RedirectToAction("AdminIndex");
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                db.Products.Add(product);
            }
            else
            {
                Product dbEntry = db.Products.Find(product.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.ShortDescription = product.ShortDescription;
                    dbEntry.FullDescription = product.FullDescription;
                    dbEntry.AdminComment = product.AdminComment;
                    dbEntry.ShowOnHomePage = product.ShowOnHomePage;
                    dbEntry.AllowCustomerReviews = product.AllowCustomerReviews;
                    dbEntry.ApprovedRatingSum = product.ApprovedRatingSum;
                    dbEntry.NotApprovedRatingSum = product.NotApprovedRatingSum;
                    dbEntry.StockQuantity = product.StockQuantity;
                    dbEntry.DisplayStockAvailability = product.DisplayStockAvailability;
                    dbEntry.DisplayStockQuantity = product.DisplayStockQuantity;
                    dbEntry.MinStockQuantity = product.MinStockQuantity;
                    dbEntry.NotifyAdminForQuantityBelow = product.NotifyAdminForQuantityBelow;
                    dbEntry.DisableBuyButton = product.DisableBuyButton;
                    dbEntry.DisableWishlistButton = product.DisableWishlistButton;
                    dbEntry.Price = product.Price;
                    dbEntry.MarkAsNew = product.MarkAsNew;
                    dbEntry.MarkAsNewStartDateTimeUtc = product.MarkAsNewStartDateTimeUtc;
                    dbEntry.MarkAsNewEndDateTimeUtc = product.MarkAsNewEndDateTimeUtc;
                    dbEntry.HasDiscountsApplied = product.HasDiscountsApplied;
                    dbEntry.Weight = product.Weight;
                    dbEntry.Length = product.Length;
                    dbEntry.Width = product.Width;
                    dbEntry.Height = product.Height;
                    dbEntry.AvailableStartDateTimeUtc = product.AvailableStartDateTimeUtc;
                    dbEntry.AvailableEndDateTimeUtc = product.AvailableEndDateTimeUtc;
                    dbEntry.Published = product.Published;
                    dbEntry.Deleted = product.Deleted;
                    dbEntry.CreatedOnUtc = product.CreatedOnUtc;
                    dbEntry.UpdatedOnUtc = product.UpdatedOnUtc;
                }
            }
            db.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = db.Products.Find(productID);
            if (dbEntry != null)
            {
                db.Products.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }
	}
}