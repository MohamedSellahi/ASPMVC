using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControllersAndActions.Infrastructure;


namespace ControllersAndActions.Controllers {
   public class DerivedController : Controller {

      public ActionResult Rename() {
         // Access various properties for the context objects 
         string userName = User.Identity.Name;
         string serverName = Server.MachineName;
         string clientIP = Request.UserHostAddress;
         DateTime dateStamp = HttpContext.Timestamp;

         return View();
      }

      public RedirectToRouteResult ProduceOutput() {
         //return new RedirectResult("Basic/Index");
         return RedirectToAction("Index", "Basic");
      }


      public ActionResult ProduceOutput2() {
         if (Server.MachineName == "TYNY") {
            return new CustomRedirectResult { Url = "Basic/Index" };
         }
         else {
            Response.Write(string.Format("Controller: Derived, Action: ProductOut2\n machine name:" +
               "{0}", Server.MachineName));
            return null;
         }
      }
      // GET: Derived
      public ActionResult Index() {
         //ViewBag.Message = "Hello from the Derived Controller Index method";
         DateTime date = DateTime.Today;
         ViewBag.Message = "Using the view bag dynamic object";
         ViewBag.Date = date;

         return View(date);
      }

      public string Hello() {
         return string.Format("Helo World: Your Machine is: {0}", Server.MachineName);
      }

     

   }
}