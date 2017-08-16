using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filters.Infrastructure;

namespace Filters.Controllers {
   public class HomeController : Controller {

      // GET: Home
      //[CustomAuth(false)]
      [Authorize(Users = "admin")]
      public string Index() {
         return "This is the Index action on the Home controller";
      }

      [GoogleAuth]
      public string List() {
         return "This is the List action on the Home controller";
      }


      [RangeExcemption]
      public string RangeTest(int id) {
         if (id > 100) {
            return String.Format("The id value is:{0}", id);
         }
         else {
            //throw new ArgumentOutOfRangeException("id", id, "");
            throw new ArgumentOutOfRangeException("id", id, "");
         }
      }

      [HandleError(ExceptionType =typeof(IndexOutOfRangeException),
         View ="RangeError")]
      public string RangeTest2(int id) {
         if (id > 100) {
            return String.Format("The id value is:{0}", id);
         }
         else {
            //throw new ArgumentOutOfRangeException("id", id, "");
            throw new ArgumentOutOfRangeException("id", id, "");
         }
      }
   }
}