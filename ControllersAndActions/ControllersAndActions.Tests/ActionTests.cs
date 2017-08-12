using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControllersAndActions.Controllers;
using System.Web.Mvc;

namespace ControllersAndActions.Tests {
   [TestClass]
   public class ActionTests {

      [TestMethod]
      public void ViewSelectionTest() {
         // Arrange 
         ExampleController target = new ExampleController();

         // aact 
         ViewResult result = target.Index();

         // assert
         Assert.AreEqual(result.ViewName, "Homepage");
         // test for appropriate model passed to a view 
         Assert.IsInstanceOfType(result.ViewData.Model, typeof(DateTime));
      }

      [TestMethod]
      public void ControllerTest() {
         // Arrange - create the controller
         ExampleController target = new ExampleController();
         // Act - call the action method
         ViewResult result = target.Index();
         // Assert - check the result
         Assert.AreEqual("Hello", result.ViewBag.Message);
      }

      [TestMethod]
      public void Can_Redirect_To_Temporary() {
         // Arrange 
         ExampleController target = new ExampleController();

         // Act 
         RedirectToRouteResult result = target.MyRedirectToRoute();

         // Assert 
         Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
         Assert.IsFalse(result.Permanent);
         Assert.AreEqual("Example", result.RouteValues["controller"]);
         Assert.AreEqual("Index", result.RouteValues["action"]);
         Assert.AreEqual("MyId", result.RouteValues["Id"]);
      }

      [TestMethod]
      public void Can_Redirect_To_Action() {
         // Arrange 
         ExampleController target = new ExampleController();

         // act
         RedirectToRouteResult result = target.MyRedirectToRoute();

         // Assert
         Assert.AreEqual("Index", result.RouteValues["action"]);
         Assert.AreEqual("Examlpe, result.RouteValues["controller"]);
         
      }

      [TestMethod]
      public void Can_Send_Status_Code() {
         // Arrange 
         ExampleController target = new ExampleController();

         // Act 
         HttpStatusCodeResult result = target.StatusCode();

         // Assert
         Assert.AreEqual(404, result.StatusCode);
      }
   }
}
