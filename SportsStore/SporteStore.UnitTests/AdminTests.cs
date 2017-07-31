using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace SportsStore.UnitTests {
   [TestClass]
   public class AdminTests {


      [TestMethod]
      public void Index_Contains_All_Products() {
         // Arrange - create the mock repo 
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
         });

         // Arrange create  acontroller 
         AdminController target = new AdminController(mock.Object);

         // Assert
         Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

         // Assert 
         Assert.AreEqual(result.Length, 3);
         Assert.AreEqual(result[0].Name, "P1");
         Assert.AreEqual(result[1].Name, "P2");
         Assert.AreEqual(result[2].Name, "P3");

      }




   }
}
