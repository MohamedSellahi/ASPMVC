using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers {
   // use route prefix to redirect urls starting with users to this controller
   [RoutePrefix("Users")]
   public class CustomerController : Controller {


      // GET: Customer
      [Route("~/Test")] // will be accessed as ~/Test
      public ActionResult Index() {
         ViewBag.Controller = "Customer";
         ViewBag.Action = "Index";
         return View("ActionName");
      }

      public ActionResult List() {
         ViewBag.Controller = "Customer";
         ViewBag.Action = "List";
         return View("ActionName");
      }


      [Route("Add/{user}/{id:int}", Name ="AddRoute")]  // note id is constained to ints
      public string Create(string user, int id) {
         return string.Format("User: {0}, ID: {1}", user, id);
      }

      [Route("{Add}/{user}/{password:alpha:length(6)}")]
      public string ChangePass(string user, string password) {
         return string.Format("ChangePass Method - User: {0}, Pass: {1}", user, password);
      }

   }
}