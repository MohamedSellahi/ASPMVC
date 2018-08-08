using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Users.Models
{
   public class AppIdentityContext:IdentityDbContext<AppUser>
   {
      public AppIdentityContext():base("IdentityDb")
      {

      }

      /// <summary>
      /// static constructor
      /// </summary>
      static AppIdentityContext()
      {
         Database.SetInitializer<AppIdentityContext>(new IdentityDbInit()); 
      }

      /// <summary>
      /// simple factory method for <see cref="AppIdentityContext"/>
      /// </summary>
      /// <returns></returns>
      public static AppIdentityContext Create()
      {
         return new AppIdentityContext();
      }
   }
}