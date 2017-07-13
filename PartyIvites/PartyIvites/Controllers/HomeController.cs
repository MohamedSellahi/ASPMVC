using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyIvites.Models;

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

      // HAndeling get requested from the RsvpForm 
      [HttpGet]
      public ActionResult RsvpForm() {
         return View();
      }

      // Handeling Post requests from RsvpForm 
      [HttpPost]
      public ActionResult RsvpForm(GuestResponse guestresponse) {
         if (ModelState.IsValid) {
         // TODO: Email response to the party organizer 
         return View("Thanks",guestresponse);
         }
         else {
            // there is a validation error
            // return to the Form view 
            return View();
         }
      }



   }
}