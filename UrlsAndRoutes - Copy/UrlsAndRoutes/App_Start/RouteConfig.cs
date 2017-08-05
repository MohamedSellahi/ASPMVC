using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Routing.Constraints;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes {
   public class RouteConfig {

      public static void RegisterRoutes(RouteCollection routes) {

         // Enable attribute based routing 
         routes.MapMvcAttributeRoutes();


         // using custom route
         // this will constain a route to additionalcontroiller Home, if the 
         // useragent string contains chrome, whatever is the content of the url (catch all)
         routes.MapRoute("CustomRoute", "{*catchall}",
            new { controller = "Home", action = "Index" },
            new { customConstraint = new UserAgentConstraint("Chrome") },
            new[] { "UrlsAndRoutes.AdditionalControllers" });

         //id = "DefaultId"}); // Default is a reserved key word 
         // type and value constraint 
         routes.MapRoute("typeAndValue2", "{controller}/{action}/{id}/{*catchall}",
           new { controller = "Home", action = "Index", id = UrlParameter.Optional },
           new {
              controller = "^H.*",
              action = "Index|About",
              httpmethod = new HttpMethodConstraint("GET"),
              id = new CompoundRouteConstraint(new IRouteConstraint[] {
              new AlphaRouteConstraint(),
              new MinLengthRouteConstraint(6)})
           },
           new[] { "UrlsAndRoutes.Controllers" });


         routes.MapRoute("typeAndValue", "{controller}/{action}/{id}/{*catchall}",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            new {
               controller = "^H.*",
               action = "Index|About",
               httpmethod = new HttpMethodConstraint("GET"),
               id = new RangeRouteConstraint(10, 20)
            },
            new[] { "UrlsAndRoutes.Controllers" });


         // constraining a route using HTTP methods 
         routes.MapRoute("RegEx3", "{controller}/{action}/{id}/{*catchall}",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            new {
               controller = "^H.*",
               action = "Index|About",
               HttpMethod = new HttpMethodConstraint("GET")
            },
            new[] { "UrlsAndRoutes.Controllers" }
            );

         // Constraining routes using regular expressions 


         routes.MapRoute("RegEx2", "{controller}/{action}/{id}/{*catchall}",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // default values 
            new { controller = "^H.*", action = "^Index$|^About$" }, //  
            new[] { "UrlsAndRoutes.Controllers" });

         routes.MapRoute("RegEx", "{controller}/{action}/{id}/{*catchall}",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // default values 
            new { controller = "^H.*" }, // constraints : this route only matches routes that begin with H 
            new[] { "UrlsAndRoutes.Controllers" });




         // support for variable length URL segments

         Route myRoute = routes.MapRoute("AddControllerRoute", "Home/{action}/{id}/{*catchall}",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            new[] { "UrlsAndRoutes.AdditionalControllers" });

         // this tell MVC frame to look only in the name space i specify 
         myRoute.DataTokens["UseNamespaceFallback"] = false;

         routes.MapRoute("MyRouteCatchAll", "{controller}/{action}/{id}/{*catchall}",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            new[] { "UrlsAndRoutes.Controllers" });
      }


      //public static void RegisterRoutes(RouteCollection routes) {

      //   ///* method 1: */
      //   // // create a new route :
      //   // // define the pattern : {controller}/{action}
      //   // // MvcRoutehandler: 
      //   // Route myRoute = new Route("{controller}/{action}", new MvcRouteHandler());

      //   // // add the route to the routes , defining a name is optional 
      //   // routes.Add("MyRoute", myRoute);


      //   /*method 2
      //    * use the MapRoute method of the routeCollection
      //    *
      //    */

      //   /**
      //    * The order of the definition is important 
      //    * first defined first tested 
      //    * so begin by the most specific route 
      //    * */

      //   // aliases for refactored controller and action 
      //   routes.MapRoute("ShopShema2", "Shop/OldAction",
      //                    new { controller = "Home", action = "Index" });

      //   // use this to preserve old URL schemas
      //   // exemple: a controller that has been renamed from shop to Home
      //   // matches any two segments URL, where the first segment is Shop.
      //   // since controller segment is not variable, the default value is used 
      //   // Shop/Index --> Home/Index;
      //   routes.MapRoute("ShopShema", "Shop/{action}",
      //                    new { controller = "Home" }); 


      //   // URL pattern with Mixed segments
      //   // Public/Home/Index -->
      //   // /XAdmin/Index --> Admin/Index
      //   routes.MapRoute("", "X{controller}/{action}");

      //   // Route with default value on both action and controller
      //   routes.MapRoute("MyRoute", "{controller}/{action}",
      //                    new {controller ="Home", action = "Index"});

      //   // URL pattern with static segments
      //   // this pattern will match URLs containing 3 segments 
      //   // where the first segment must be "Public"
      //   routes.MapRoute("", "Public/{controller}/{action}",
      //                   new { controller = "Home", action = "Index" });

      //   // a route with default behaviour ~/Admin
      //   //routes.MapRoute("MyRouteWithDefault", "{controller}/{action}",
      //   //                 new { action = "Index"});


      //   //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      //   //routes.MapRoute(
      //   //    name: "Default",
      //   //    url: "{controller}/{action}/{id}",
      //   //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
      //   //);
      //}



   }
}
