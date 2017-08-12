using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers {
   public class ExampleController : Controller {

      // GET: Example
      public ViewResult Index() {
         ViewBag.Message = TempData["Message"]?? "Hello";
         ViewBag.Date = TempData["Date"] == null? DateTime.Today: (DateTime)TempData["Date"];
         return View();
      }

      public RedirectToRouteResult RedirectAndPassData() {
         // Use temp property 
         TempData["Message"] = "Hello from redirect";
         TempData["Date"] = DateTime.Now;
         return RedirectToAction("Index");
      }

      public RedirectResult Redirect() {
         return Redirect("Example/Index");
      }

      public RedirectToRouteResult MyRedirectToRoute() {
         return RedirectToRoute(
            new {
               controller = "Example",
               action = "Index",
               Id = "MyId"
            }
            );
      }

      // 
      public RedirectToRouteResult MyRedirectToAction() {
         return RedirectToAction("Index", "Basic");
      }

      // Send status code 
      public HttpStatusCodeResult StatusCode() {
         //return new HttpStatusCodeResult(404, "URL cannot be serviced");
         //return new HttpNotFoundResult("URL cannot be serviced");
         return HttpNotFound("URL cannot be serviced");
      }
   }
}