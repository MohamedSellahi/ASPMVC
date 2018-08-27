using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using Users.Infrastructure;

namespace Users.Models
{
   public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
   {
     
   }
}
