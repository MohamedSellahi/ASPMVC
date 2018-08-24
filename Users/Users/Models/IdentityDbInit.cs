using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using Users.Infrastructure;

namespace Users.Models
{
   public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
   {
      protected override void Seed(AppIdentityDbContext context)
      {
         PerformeInitialSetup(context);
         base.Seed(context);
      }

      /// <summary>
      /// performs initial setup : creates admin role and a single user 
      /// </summary>
      /// <param name="context"></param>
      private void PerformeInitialSetup(AppIdentityDbContext context)
      {
         AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
         AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));
         string roleName = "Administrators";
         string userName = "Admin";
         string password = "MySecret";
         string email = "admin@example.com";
         if (!roleMgr.RoleExists(roleName))
         {
            roleMgr.Create(new AppRole(roleName));
         }
         AppUser user = userMgr.FindByName(userName);
         if (user == null)
         {
            userMgr.Create(new AppUser { UserName = userName, Email = email },
            password);
            user = userMgr.FindByName(userName);
         }

         if (!userMgr.IsInRole(user.Id, roleName))
         {
            userMgr.AddToRole(user.Id, roleName);
         }
      }
   }
}
