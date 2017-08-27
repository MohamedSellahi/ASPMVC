using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelperMethods.Models;

namespace HelperMethods.Controllers {
   public class PeopleController : Controller {

      private Person[] personData = {
         new Person {FirstName = "Adam", LastName = "Freeman", Role = Role.Admin},
         new Person {FirstName = "Jacqui", LastName = "Griffyth", Role = Role.User},
         new Person {FirstName = "John", LastName = "Smith", Role = Role.User},
         new Person {FirstName = "Anne", LastName = "Jones", Role = Role.Guest}
      };


      // GET: People
      public ActionResult Index() {
         return View();
      }


      //public ActionResult getPeople() {
      //   return View(personData);
      //}

      public ActionResult getPeople(string selectedRole = "All") {
         return View((object)selectedRole);
      }

      //[HttpPost]
      //public ActionResult getPeople(string selectedRole) {
      //   if (selectedRole == null || selectedRole == "All") {
      //      return View(personData);
      //   }
      //   else {
      //      Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
      //      return View(personData.Where(p => p.Role == selected));
      //   }
      //}

      public PartialViewResult GetPeopleData(string selectedRole = "All") {
         IEnumerable<Person> data = personData;
         if (selectedRole != "All") {
            Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
            data = personData.Where(p => p.Role == selected);
         }
         return PartialView(data);
      }
   }
}