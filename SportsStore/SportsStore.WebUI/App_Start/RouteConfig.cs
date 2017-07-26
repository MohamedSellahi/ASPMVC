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

         // Routes for pagination with category 
         //default value for route pagination
         // exemple: ~/
         routes.MapRoute(null,
            "",
            new {
               controller = "Product",
               action = "List",
               category = (string)null,
               page = 1
            });

         // only page is specified : ~/?page=3
         routes.MapRoute(null,
            "Page{page}",
            new { controller = "Product", action = "List", category = (string)null },
            new { page = @"\d+" }
            );

         // only category is specified: ~/category
         routes.MapRoute(null,
            "{category}",
            new {
               controller = "Product",
               action = "List",
               page = 1
            });

         // both category and page are specified: ~/category/Page{1}
         routes.MapRoute(null,
            "{category}/Page{page}",
            new { controller = "Product", action = "List" },
            new { page = @"\d+" }  // page must be a number 
            );

         routes.MapRoute(null, "{controller}/{action}");

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
