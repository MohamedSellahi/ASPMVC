using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers {

   public class HomeController : Controller {


      // GET: Home
      public ActionResult Index() {
         ViewBag.Controller = "Home";
         ViewBag.Action = "Index";
         return View("ActionName");
      }

      // accessing the route data using RouteData.Values 
      public ActionResult CustomVariable(string id = "DefaultId") {
         ViewBag.Controller = "Home";
         ViewBag.Action = "CustomVariable";
         // this matches the route with optionnal id
         //ViewBag.CustomVariable = id ?? "<no value>";

         // Avoid using the test for null: the defautl value is defined in the controller 
         // not in the route definition. Important for separation of concerns 
         ViewBag.CustomVariable = id;
         ViewBag.CatchAll = RouteData.Values["catchall"] ?? "<no value>"; 
         
         //ViewBag.CustomVariable = RouteData.Values["id"];
         return View(); // return default view (CustomVariable.cshtml
      }

      public RedirectToRouteResult MyActionMethod() {
         string myActionUrl = Url.Action("Index", new { id = "MyId" });
         string myRouteUrl = Url.RouteUrl(new { controller = "Home", action = "Index" });
         return RedirectToRoute(new { controller = "Home", action = "Index", id = "MyID" });
      }



   }
}