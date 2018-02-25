using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiltersRevisited.Infrastructure {
   public class ProfileActionAttribute: FilterAttribute, IActionFilter {
      Stopwatch timer;

      public void OnActionExecuted(ActionExecutedContext filterContext) {
         timer.Stop();
         if (filterContext.Exception == null) {
            filterContext.HttpContext.Response.Write(String.Format("<div>action method elapsed time: {0:F6}</div>",timer.Elapsed.TotalSeconds));
         }
      }

      public void OnActionExecuting(ActionExecutingContext filterContext) {
         timer = new Stopwatch();
         timer.Start();
      }
   }
}