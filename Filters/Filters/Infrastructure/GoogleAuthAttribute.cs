using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;


namespace Filters.Infrastructure {
   public class GoogleAuthAttribute : FilterAttribute, IAuthenticationFilter {


      public void OnAuthentication(AuthenticationContext filterContext) {
         throw new NotImplementedException();
      }

      public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) {
         if (filterContext.Result == null) {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
               {"controller","GoogleAccount" },
               {"action","Login" },
               {"returnUrl", filterContext.HttpContext.Request.RawUrl }
            });
         }
      }


   }
}