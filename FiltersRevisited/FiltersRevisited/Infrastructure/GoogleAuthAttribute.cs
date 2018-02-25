using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;

namespace FiltersRevisited.Infrastructure {
   public class GoogleAuthAttribute : FilterAttribute, IAuthenticationFilter {

      public void OnAuthentication(AuthenticationContext filterContext) {
         //throw new NotImplementedException();
         IIdentity ident = filterContext.Principal.Identity;
         if (!ident.IsAuthenticated || !ident.Name.EndsWith("@google.com")) {
            // verify authentified users with google account
            filterContext.Result = new HttpUnauthorizedResult(); // --> this will cause MVC to call OnAuthenticationChanllange
         }

      }

      public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) {
         if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult) { // to avoid second chalange after action method is executed
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
               {"controller", "GoogleAccount"},
               {"action","Login"},
               {"returnUrl",filterContext.HttpContext.Request.RawUrl}
            });
         }
         else {
            FormsAuthentication.SignOut(); // will be executed once the call to the method is over. 
         }
      }
   }
}