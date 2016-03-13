using EcommerceWebsite.Models;
using EcommerceWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceWebsite.Controllers
{
    public class CartController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();

        
        EmailOrderProcessor orderProcessor;
        public CartController()
        {
            orderProcessor = new EmailOrderProcessor(new EmailSettings());
        }
        
        //
        // GET: /Cart/
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl }); 
        }

      
        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == Id);
            if (product != null)
            {
                cart.AddItem(product, 1);
                //GetCart().AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
                //GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [Authorize]
        public ViewResult Checkout(Cart cart, string returnUrl)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
                return View();
            }

            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            
            if (ModelState.IsValid)
            {
                try {
                    //save address
                    Address address = new Address();
                    address.FirstName = shippingDetails.FirstName;
                    address.LastName = shippingDetails.LastName;
                    address.Address1 = shippingDetails.Line1;
                    address.Address2 = shippingDetails.Line2;
                    address.City = shippingDetails.City;
                    address.State = shippingDetails.State;
                    address.Country = shippingDetails.Country;
                    address.ZipPostalCode = shippingDetails.Zip;
                    address.CreatedOnUtc = DateTime.Now;
                    db.Addresses.Add(address);
                    db.SaveChanges();

                    //retreive customer details
                    var userName=HttpContext.User.Identity.Name;
                    var customer = db.Customers.Where(x => x.Username == userName).FirstOrDefault();
                    customer.ShippingAddress_Id = address.Id;
                    customer.BillingAddress_Id = address.Id;
                    db.SaveChanges();
                
                    //save order
                    Order order = new Order();
                    order.OrderGuid = Guid.NewGuid();
                    order.CustomerId = customer.Id;
                    order.BillingAddressId = address.Id;
                    order.ShippingAddressId = address.Id;
                    order.Deleted = false;
                    order.CreatedOnUtc = DateTime.Now;
                    order.OrderTotal = cart.ComputeTotalValue();
                    db.Orders.Add(order);
                    db.SaveChanges();

                    //save order details
                    foreach (var line in cart.Lines)
                    {
                        OrderItem orderItem = new OrderItem();
                        orderItem.OrderItemGuid = Guid.NewGuid();
                        orderItem.OrderId = order.Id;
                        orderItem.ProductId = line.Product.Id;
                        orderItem.Quantity = line.Quantity;
                        db.OrderItems.Add(orderItem);              
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                orderProcessor.ProcessOrder(cart, shippingDetails);
                var cartInput = new CartIndexViewModel { Cart = cart, ReturnUrl = null };
                //cart.Clear();

                //Variable to display Shipping Information
                ViewBag.Fname = shippingDetails.FirstName;
                ViewBag.Lname = shippingDetails.LastName;
                ViewBag.Line1 = shippingDetails.Line1;
                ViewBag.Line2 = shippingDetails.Line2;
                ViewBag.City = shippingDetails.City;
                ViewBag.State = shippingDetails.State;
                ViewBag.Country = shippingDetails.Country;
                ViewBag.Zip = shippingDetails.Zip;

                return View("Completed", cartInput);
            }
            else
            {
                return View(shippingDetails);
            }
        }

        /*
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }*/

	}
}