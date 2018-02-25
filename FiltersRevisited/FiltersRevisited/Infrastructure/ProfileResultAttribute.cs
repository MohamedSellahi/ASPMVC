using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiltersRevisited.Infrastructure {
   public class ProfileResultAttribute : FilterAttribute, IResultFilter {
      Stopwatch timer;

      public void OnResultExecuted(ResultExecutedContext filterContext) {
         timer.Stop();
         if (filterContext.Exception == null) {
            filterContext.HttpContext.Response.Write(String.Format("<div>Result method elapsed time: {0:F6}</div>", timer.Elapsed.TotalSeconds));
         }
      }

      public void OnResultExecuting(ResultExecutingContext filterContext) {
         timer = new Stopwatch();
         timer.Start();
      }
   }
}