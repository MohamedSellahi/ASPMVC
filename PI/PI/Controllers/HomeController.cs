using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PI.Models;

namespace PI.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ViewResult Index() {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good afternoon";
            return View();
        }


        // to be called for get requests 
        [HttpGet]
        public ViewResult RsvpForm() {
            return View();
        }


        [HttpPost]
        public ViewResult RsvpForm(GuestResponce guestResponce) {
            // todo: Email responce to party organizer 
            // use validation rules 
            if(ModelState.IsValid)
            {
                return View("Thanks", guestResponce);
            }
            else
            {
                // data not valid: display form 
                return View();
            }
        }
    }


}