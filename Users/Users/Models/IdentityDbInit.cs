using System;
using System.Data.Entity;

namespace Users.Models
{
   public class IdentityDbInit : DropCreateDatabaseAlways<AppIdentityDbContext>
   {
      public override void InitializeDatabase(AppIdentityDbContext context)
      {
         PerformeInitialSetup(context);
         base.Seed(context);
      }
      /// <summary>
      /// performs initial setup 
      /// </summary>
      /// <param name="context"></param>
      private void PerformeInitialSetup(AppIdentityDbContext context)
      {
         // init will go here 
      }
   }
}