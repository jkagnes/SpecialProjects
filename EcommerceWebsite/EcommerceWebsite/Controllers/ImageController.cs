using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace EcommerceWebsite.Controllers
{
    public class ImageController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();
        //
        // GET: /Image/
        public ActionResult show(int id)
        {            
            var picture = db.Product_Picture_Mapping.Include(x => x.Picture)
                                    .Where(x => x.ProductId == id)
                                    .FirstOrDefault();
            return File(picture.Picture.PictureBinary, picture.Picture.MimeType);
        }
	}
}