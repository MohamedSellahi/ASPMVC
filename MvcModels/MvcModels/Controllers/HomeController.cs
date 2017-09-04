using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcModels.Models;

namespace MvcModels.Controllers {
   public class HomeController : Controller {
      private Person[] personData = {
         new Person {PersonId = 1, FirstName = "Adam", LastName = "Freeman", Role = Role.Admin},
         new Person {PersonId = 2, FirstName = "Jacqui", LastName = "Griffyth",Role = Role.User},
         new Person {PersonId = 3, FirstName = "John", LastName = "Smith", Role = Role.User},
         new Person {PersonId = 4, FirstName = "Anne", LastName = "Jones", Role = Role.Guest}
         };


      // GET: Home
      // setting nullable parametter provides a fallback mechanism to the model binder 
      public ActionResult Index(int id = 1) {
         Person dataItem = personData.Where(p => p.PersonId == id).First();
         return View(dataItem);
      }

      // Binding complex data 
      public ActionResult CreatePerson() {
         return View(new Person());
      }

      [HttpPost]
      public ActionResult CreatePerson(Person model) {
         return View("Index", model);
      }

      // use the bind attribute to tell MVC how to partially bind 
      // the model
      // [Bind(Prefix ="HomeAddress", Exclude ="Country")]
      public ActionResult DisplaySummary([Bind(Prefix ="HomeAddress")]AddressSummary summary) {
         return View(summary);
      }
   }
}