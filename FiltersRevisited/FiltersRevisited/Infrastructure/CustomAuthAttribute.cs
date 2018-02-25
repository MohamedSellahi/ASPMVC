using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiltersRevisited.Infrastructure {
   public class CustomAuthAttribute:AuthorizeAttribute {
      bool localAllowed;
      public CustomAuthAttribute(bool allowedParam = false) {
         localAllowed = allowedParam;
      }

      protected override bool AuthorizeCore(HttpContextBase httpContext) {
         if (httpContext.Request.IsLocal) {
            return localAllowed;
         }
         else {
            return true;
         }
      }

   }
}