using FiltersRevisited.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FiltersRevisited.Controllers {
   public class HomeController : Controller {
      // GET: Home
      //[CustomAuth(allowedParam:true)]
      [Authorize(Users ="admin")]// need authentication , and role as admin
      public string Index() {
         return "The is the index action of the Home controller";
      }

      [GoogleAuth]
      public string List() {
         return "This is the list action on the Home controller";
      }

      //[RangeException]
      [HandleError(ExceptionType =typeof(ArgumentOutOfRangeException),View ="RangeError")]
      public string RangeTest(int id) {
         if (id < 100) {
            throw new ArgumentOutOfRangeException("id", id, "");
         }
         else {
            return String.Format("The id value is: {0}", id);
         }
      }

      //[CustomAction]
      [ProfileAction]
      [ProfileResult]
      [ProfileAll]
      public string FilterTest() {
         //Thread.Sleep(1000);
         return "This is Filter test action";
      }
   }
}