using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure {
   public class UserAgentConstraint : IRouteConstraint {

      private string _requiredUserAgent;

      public UserAgentConstraint(string agentParam) {
         _requiredUserAgent = agentParam;
      }
      public bool Match(HttpContextBase httpContext,
         Route route,
         string parameterName,
         RouteValueDictionary values,
         RouteDirection routeDirection) {

         return httpContext.Request.UserAgent != null &&
            httpContext.Request.UserAgent.Contains(_requiredUserAgent);
      }
   }
}