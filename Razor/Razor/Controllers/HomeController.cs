using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Razor.Models;

namespace Razor.Controllers {
   public class HomeController : Controller {

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

      public ActionResult NameAndPrice() {
         return View(_myProduct);
      }
   }
}