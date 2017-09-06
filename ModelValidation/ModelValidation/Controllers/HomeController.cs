using ModelValidation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelValidation.Controllers {
   public class HomeController : Controller {
      public ViewResult makeBooking() {
         return View(new Appointment() { Date = DateTime.Now });
      }

      [HttpPost]
      public ViewResult makeBooking(Appointment appt) {
         //Explicite validation of the model 
         if (string.IsNullOrEmpty(appt.ClientName)) {
            ModelState.AddModelError("ClientName", "Please enter your name");
         }

         if (ModelState.IsValidField("Date") && DateTime.Now > appt.Date) {
            ModelState.AddModelError("Date", "Plase enter a date in the future");
         }

         if (!appt.TermsAccepted) {
            ModelState.AddModelError("TermsAccepted", "You must accept the terms");
         }
         if (ModelState.IsValid) {
            // Store new appointment here 
            return View("Completed", (object)appt);
         }
         else {
            return View();
         }
      }

      // GET: Home
      //public ActionResult Index() {
      //   return View();
      //}
   }
}