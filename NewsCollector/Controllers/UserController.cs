using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsCollector.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = ReturnUserType();

                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();

        }
        [Authorize]
        public ActionResult MyAccount()
        {
            ViewBag.Name = User.Identity.Name;
            if (User.IsInRole("Regular"))
            {
                ViewBag.AccountType = "Bez subskrybcji";
                return View();
            }
            if (User.IsInRole("Subscriber"))
            {
                ViewBag.AccountType = "Subskrybent";
                return View();
            }
            if (User.IsInRole("Redactor"))
            {
                return RedirectToAction("Index","Redactor");
            }
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        public string ReturnUserType()
        {
            var user = User.Identity;
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var s = UserManager.GetRoles(user.GetUserId());
            return s[0].ToString();
        }
    }
}