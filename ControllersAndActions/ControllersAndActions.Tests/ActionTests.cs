using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControllersAndActions.Controllers;
using System.Web.Mvc;

namespace ControllersAndActions.Tests {
   [TestClass]
   public class ActionTests {
   
      [TestMethod]
      public void ControllerTest() {
         // Arrange 
         ExampleController target = new ExampleController();

         // aact 
         ViewResult result = target.Index();

         // assert
         Assert.AreEqual(result.ViewName, "Homepage");
      }
   }
}
