using EcommerceWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace EcommerceWebsite.Controllers
{
    public class AccountController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();
        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Customers.Include("CustomerRole")
                                                        .Where(x => x.Username == loginViewModel.UserName &&
                                                                    x.Password == loginViewModel.Password).FirstOrDefault();

                
                if (customer != null)
                {
                    //FormsAuthentication.SetAuthCookie(loginViewModel.UserName, false);


                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, loginViewModel.UserName, DateTime.Now, DateTime.Now.AddMinutes(2880), false, 
                        customer.CustomerRole.Name, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);

//                    Response.Redirect(FormsAuthentication.GetRedirectUrl(Login1.UserName, Login1.RememberMeSet));


                    return Redirect(returnUrl ?? Url.Action("Home", "Home"));
                }
                else { 
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else {
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Home", "Home");
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Customer customer = new Customer();
                    customer.CustomerGuid = Guid.NewGuid();
                    customer.FirstName = registerViewModel.FirstName;
                    customer.LastName = registerViewModel.LastName;
                    customer.Password = registerViewModel.Password;
                    customer.Email = registerViewModel.Email;
                    customer.Username = registerViewModel.UserName;
                    customer.CreatedOnUtc = DateTime.Now;
                    customer.RoleId = 2;

                    db.Customers.Add(customer);
                    db.SaveChanges();
                }

                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            
                return RedirectToAction("Login");
            }
            return View();
        }
	}
}