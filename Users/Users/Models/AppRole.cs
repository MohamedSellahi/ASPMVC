using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
   public class AppRole : IdentityRole
   {
      public AppRole(): base()
      {

      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="name">the name of the role</param>
      public AppRole(string name):base(name)
      {
      }
   }
}