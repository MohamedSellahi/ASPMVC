using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace UrlsAndRoutes.Tests {

   [TestClass]
   public class RouteTests {

      // create a mock HttpContextBase object 
      private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET") {
         // Create a mock request 
         Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();

         // this adds ~ devant l url : ~/Admin/Index 
         mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
         // set the HTTP method
         mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

         // create a mock response 
         Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();

         mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

         //create the mock context, using the request and the response 
         Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
         mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
         mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

         // return the mocked context 
         return mockContext.Object;
      }

      // perfom a batch test 
      private void TestRouteMatch(string url, string controller, string action,
         object routeProperties = null, string httpMethod = "GET") {

         // Arrange 
         RouteCollection routes = new RouteCollection();
         RouteConfig.RegisterRoutes(routes);
         // Act - process the route
         RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));
         // Assert
         Assert.IsNotNull(result);
         Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));

      }

      private bool TestIncomingRouteResult(RouteData routeResult, string controller,
         string action, object propertySet = null) {

         Func<object, object, bool> valCompare = (v1, v2) => {
            return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
         };

         bool result = valCompare(routeResult.Values["controller"], controller) &&
            valCompare(routeResult.Values["action"], action);

         if (propertySet != null) {
            PropertyInfo[] proInfo = propertySet.GetType().GetProperties();
            foreach (PropertyInfo pi in proInfo) {
               if (!(routeResult.Values.ContainsKey(pi.Name) &&
                    valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null)))) {
                  result = false;
                  break;
               }
            }
         }
         return result;
      }

      private void TestRouteFail(string url) {
         // Arrange
         RouteCollection routes = new RouteCollection();
         RouteConfig.RegisterRoutes(routes);
         // Act - process the route
         RouteData result = routes.GetRouteData(CreateHttpContext(url));
         // Assert
         Assert.IsTrue(result == null || result.Route == null);
      }

      [TestMethod]
      public void TestIncomingRoutes() {
         // check for the URL that is hoped for
         TestRouteMatch("~/Admin/Index", "Admin", "Index");
         // check that the values are being obtained from the segments
         TestRouteMatch("~/One/Two", "One", "Two");
         // ensure that too many or too few segments fails to match
         TestRouteFail("~/Admin/Index/Segment");
         TestRouteFail("~/Admin");

         TestRouteMatch("~/", "Home", "Index");
         TestRouteMatch("~/Customer", "Customer", "Index");
         TestRouteMatch("~/Customer/List", "Customer", "List");
         TestRouteFail("~/Customer/List/All");
      }
   }
}
