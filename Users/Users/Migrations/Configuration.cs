namespace Users.Migrations
{
   using Microsoft.AspNet.Identity;
   using Microsoft.AspNet.Identity.EntityFramework;
   using System;
   using System.Data.Entity;
   using System.Data.Entity.Migrations;
   using System.Linq;
   using Users.Infrastructure;
   using Users.Models;

   internal sealed class Configuration : DbMigrationsConfiguration<Models.AppIdentityDbContext>
   {
      public Configuration()
      {
         AutomaticMigrationsEnabled = true;
         ContextKey = "Users.Models.AppIdentityDbContext";
      }

      protected override void Seed(Models.AppIdentityDbContext context)
      {
         //  This method will be called after migrating to the latest version.

         //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
         //  to avoid creating duplicate seed data. E.g.
         //
         //    context.People.AddOrUpdate(
         //      p => p.FullName,
         //      new Person { FullName = "Andrew Peters" },
         //      new Person { FullName = "Brice Lambson" },
         //      new Person { FullName = "Rowan Miller" }
         //    );
         //
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

         foreach (AppUser dbUser in userMgr.Users)
         {
            if (dbUser.Country == Countries.NONE)
            {
               dbUser.SetCountryFromCity(dbUser.City);
            }
         }

         context.SaveChanges();
      }
   }
}
