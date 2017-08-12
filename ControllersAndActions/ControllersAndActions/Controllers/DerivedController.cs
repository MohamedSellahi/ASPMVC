using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

      public void ProduceOutput() {

         if (Server.MachineName == "TINY") {
            Response.Redirect("/Basic/Index");
         }
         else {
            Response.Write("Controller: Derived, Action: ProductOutpt");
         }
      }

      // GET: Derived
      public ActionResult Index() {
         ViewBag.Message = "Hello from the Derived Controller Index method";
         return View("myView");
      }

      public string Hello() {
         return string.Format("Helo World: Your Machine is: {0}",Server.MachineName);
      }
   }
}