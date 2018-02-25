﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiltersRevisited.Infrastructure {
   public class ProfileAllAttribute : ActionFilterAttribute {
      Stopwatch timer;

      public override void OnActionExecuting(ActionExecutingContext filterContext) {
         timer = Stopwatch.StartNew();
      }

      public override void OnResultExecuted(ResultExecutedContext filterContext) {
         timer.Stop();
         filterContext.HttpContext.Response.Write(string.Format("<div>Total elapsed time: {0:F6}</div>", timer.Elapsed.TotalSeconds));
      }
   }
}