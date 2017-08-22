using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Views.Infrastructre;

namespace Views {
   public class MvcApplication : System.Web.HttpApplication {
      protected void Application_Start() {
         AreaRegistration.RegisterAllAreas();
         RouteConfig.RegisterRoutes(RouteTable.Routes);

         // registering my custom ViewEngine
         ViewEngines.Engines.Insert(0,new DebugDataViewEngine());
      }
   }
}
