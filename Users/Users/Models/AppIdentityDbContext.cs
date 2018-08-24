using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Users.Models
{
   public class AppIdentityDbContext:IdentityDbContext<AppUser>
   {
      public AppIdentityDbContext():base("IdentityDb")
      {

      }

      /// <summary>
      /// static constructor
      /// </summary>
      static AppIdentityDbContext()
      {
         Database.SetInitializer(new IdentityDbInit());
      }

      /// <summary>
      /// simple factory method for <see cref="AppIdentityDbContext"/>
      /// </summary>
      /// <returns></returns>
      public static AppIdentityDbContext Create()
      {
         return new AppIdentityDbContext();
      }
   }
}