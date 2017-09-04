using HelperMethods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

      //public PartialViewResult getPeopleData(string selectedRole ="All") {
      //   IEnumerable<Person> data = personData;
      //   if (selectedRole != "All") {
      //      Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
      //      data = personData.Where(p => p.Role == selected);
      //   }
      //   return PartialView(data);
      //}

      private IEnumerable<Person> getData(string selectedRole) {
         IEnumerable<Person> data = personData;
         if (selectedRole !="All") {
            Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
            data = personData.Where(p => p.Role == selected);
         }
         return data;
      }

      //public JsonResult getPeopleDataJSON(string selectedRole = "All") {
      //   IEnumerable<Person> data = getData(selectedRole);
      //   return Json(data, JsonRequestBehavior.AllowGet);
      //}

      public JsonResult getPeopleDataJSON(string selectedRole = "All") {
         var data = getData(selectedRole).Select(p => new {
            FirstName = p.FirstName,
            LastName = p.LastName,
            Role = Enum.GetName(typeof(Role), p.Role)
         });
         return Json(data, JsonRequestBehavior.AllowGet);
      }

      public PartialViewResult getPeopleData(string selectedRole = "All") {
         return PartialView(getData(selectedRole));
      }

      //public ActionResult getPeopleData(string selectedRole = "All") {
      //   IEnumerable<Person> data = personData;
      //   if (selectedRole != "All") {
      //      Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
      //      data = personData.Where(p => p.Role == selected);
      //   }
      //   if (Request.IsAjaxRequest()) {
      //      var formattedData = data.Select(p => new {
      //         LastName = p.LastName,
      //         FirstName = p.FirstName,
      //         Role = Enum.GetName(typeof(Role), p.Role)
      //      });
      //      return Json(formattedData, JsonRequestBehavior.AllowGet);
      //   }
      //   else {
      //      return PartialView(data);
      //   }
      //}

   }
}