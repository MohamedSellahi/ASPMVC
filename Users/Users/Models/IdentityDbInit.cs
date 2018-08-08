using System;
using System.Data.Entity;

namespace Users.Models
{
   public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityContext>
   {
      public override void InitializeDatabase(AppIdentityContext context)
      {
         PerformeInitialSetup(context);
         base.Seed(context);
      }
      /// <summary>
      /// performs initial setup 
      /// </summary>
      /// <param name="context"></param>
      private void PerformeInitialSetup(AppIdentityContext context)
      {
         // init will go here 
      }
   }
}