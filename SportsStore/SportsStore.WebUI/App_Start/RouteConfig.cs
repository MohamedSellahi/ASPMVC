using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI {
   public class RouteConfig {
      public static void RegisterRoutes(RouteCollection routes) {
         routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

         // Routes for pagination in Product list 
         routes.MapRoute(
            name: null,
            url: "Page{page}", // this will transform a request like: ?page=2 to Page2
            defaults: new { Controller = "Product", action = "List" }
            );

         routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             // 
             defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
         );
      }
   }
}
