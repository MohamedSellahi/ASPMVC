using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServices.Models;

namespace WebServices.Controllers {
   public class HomeController : Controller {

      private ReservationRepository repo = ReservationRepository.Curent;


      // GET: Home
      public ActionResult Index() {
         
         return View(repo.GetAll());
      }

      public ActionResult Add(Reservation item) {
         if (ModelState.IsValid) {
            repo.Add(item);
            return RedirectToAction("Index");
         }
         else {
            return View("Index");
         }
      }

      public ActionResult Remove(int id) {
         repo.Remove(id);
         return RedirectToAction("index");
      }

      public ActionResult Update(Reservation item) {
         if (ModelState.IsValid) {
            return RedirectToAction("Index");
         }
         else {
            return View("Index");
         }
      }
   }
}