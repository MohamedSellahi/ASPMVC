using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyIvites.Controllers {
   public class HomeController : Controller {

     
      // GET: Home
      public ActionResult Index() {

         // passing some data to the view 
         int hour = DateTime.Now.Hour;
         ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
         ViewBag.Message = "Here is another message";
         return View();
      }

   }
}