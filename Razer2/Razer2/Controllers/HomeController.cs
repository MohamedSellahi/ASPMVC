using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Razer2.Models;

namespace Razer2.Controllers {
   public class HomeController : Controller {
      // GET: Home
      Product _myProduct = new Product {
         Name = "Kayak",
         Category = "Watersport",
         Price = 275M,
         Description = "a boat for one person ",
         ProductID = 1
      };

      // GET: Home
      public ActionResult Index() {
         return View(_myProduct);
      }

      public ViewResult NameAndPrice() {
         return View(_myProduct);
      }

      public ViewResult DemoExpression() {
         ViewBag.ProductCount = 1;
         ViewBag.ExpressShip = true;
         ViewBag.ApplyDiscount = false;
         ViewBag.Supplier = null; 

         return View(_myProduct);
      }

      public ViewResult DemoArray() {
         Product[] arr = {
            new Product {Name = "Kayak", Price = 275M},
            new Product {Name = "Lifejacket", Price = 48.95M},
            new Product {Name = "Soccer ball", Price = 19.50M},
            new Product {Name = "Corner flag", Price = 34.95M}
         };
         return View(arr);
      }

   }
}