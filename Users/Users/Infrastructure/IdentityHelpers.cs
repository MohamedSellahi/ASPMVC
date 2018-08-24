using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Users.Infrastructure
{
   public static class IdentityHelpers
   {
      public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
      {
         AppUserManager mgr = HttpContext.Current.GetOwinContext()?.Get<AppUserManager>();
         if (mgr != null)
         {
            var t = mgr.FindByIdAsync(id);
            t.Wait();
            return new MvcHtmlString(t.Result.UserName);
         }
         return new MvcHtmlString(String.Empty);
      }
   }
}