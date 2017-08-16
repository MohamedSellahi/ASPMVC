using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure {
   public class RangeExcemptionAttribute : FilterAttribute, IExceptionFilter {

      //public void OnException(ExceptionContext filterContext) {
      //   if (!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException) {
      //      filterContext.Result = new RedirectResult("~/Content/RangeErrorPage.html");
      //      filterContext.ExceptionHandled = true;
      //   }
      //}
      public void OnException(ExceptionContext filterContext) {
         if (!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException) {
            int val = (int)(filterContext.Exception as ArgumentOutOfRangeException).ActualValue;
            filterContext.Result = new ViewResult {
               ViewName = "RangeError",
               ViewData = new ViewDataDictionary<int>(val)
            };
            filterContext.ExceptionHandled = true;
            
         }
      }
   }
}