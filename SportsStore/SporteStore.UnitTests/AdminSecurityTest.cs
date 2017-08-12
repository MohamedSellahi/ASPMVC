using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;
using Moq;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System.Web.Mvc;

namespace SportsStore.UnitTests {
   /// <summary>
   /// Description résumée pour AdminSecurityTest
   /// </summary>
   [TestClass]
   public class AdminSecurityTest {

      [TestMethod]
      public void Can_Login_With_Valid_Credentials() {
         // Arrange create a mock authentication provider 
         Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
         mock.Setup(m => m.Athenticate("admin", "12345")).Returns(true);

         // Arrange - create the view model 
         LoginViewModel model = new LoginViewModel {
            UserName = "admin",
            Password = "12345"
         };

         // Arrange - create a account controller 
         AccountController target = new AccountController(mock.Object);

         // Act 
         ActionResult result = target.Login(model, "/myURL");

         // Assert
         Assert.AreEqual("/myURL", ((RedirectResult)result).Url);
         Assert.IsInstanceOfType(result, typeof(RedirectResult));

      }

      [TestMethod]
      public void Can_not_Login_With_Invalide_credentials() {
         // Arrange - create a mock authentication provider
         Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
         mock.Setup(m => m.Athenticate("badUser", "badPass")).Returns(false);
         // Arrange - create the view model
         LoginViewModel model = new LoginViewModel {
            UserName = "badUser",
            Password = "badPass"
         };

         // Arrange - create the controller 
         AccountController target = new AccountController(mock.Object);

         // Act - authenticate using valid model 
         ActionResult result = target.Login(model, "/MyURL");

         // Assert 
         Assert.IsInstanceOfType(result, typeof(ViewResult));
         Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);

      }


   }
}
