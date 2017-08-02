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

      [TestMethod]
      public void Can_Edit_Product() {

         // Arrange - create mock repo 
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
         });

         // Arrange - create a controller 
         AdminController target = new AdminController(mock.Object);

         // Act - 
         Product p1 = target.Edit(1).ViewData.Model as Product;
         Product p2 = target.Edit(2).ViewData.Model as Product;
         Product p3 = target.Edit(3).ViewData.Model as Product;

         // Assert
         Assert.AreEqual(1, p1.ProductID);
         Assert.AreEqual(2, p2.ProductID);
         Assert.AreEqual(3, p3.ProductID);
      }

      [TestMethod]
      public void Can_Edit_nonexistant_Product() {

         // Arrange - create mock repo 
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
         });

         // Arrange - create a controller 
         AdminController target = new AdminController(mock.Object);

         //act 
         Product result = target.Edit(4).ViewData.Model as Product;

         // Assert
         Assert.IsNull(result);
      }

//       For the POST-processing Edit action method, i need to make sure that valid updates to the Product object that
//       the model binder has created are passed to the product repository to be saved.i also want to check that invalid
//       updates (where a model error exists) are not passed to the repository.
      [TestMethod]
      public void Can_Save_Valid_Changes() {
         //Arrange - create amock repo 
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         AdminController target = new AdminController(mock.Object);
         Product product = new Product { Name = "test" };

         // Act - try to save product 
         ActionResult result = target.Edit(product);
         mock.Verify(m => m.SaveProduct(product));

         //Assert 
         Assert.IsNotInstanceOfType(result, typeof(ViewResult)); 
      }

      [TestMethod]
      public void Cannot_Save_Invalid_Changes() {

         // Arrange - create mock repo
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         AdminController target = new AdminController(mock.Object);
         Product product = new Product { Name = "Test" };

         // Add error to model state 
         target.ModelState.AddModelError("Error", "error");

         // Act try to save product
         ActionResult result = target.Edit(product);

         // Assert - check repo was not called 
         mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());

         // Assert - check method result type 
         Assert.IsInstanceOfType(result, typeof(ViewResult));
      }

      [TestMethod]
      public void Can_Delete_Valid_Product() {
         // Arrange - create a product 
         Product product = new Product() { ProductID = 2, Name = "Test" };

         // Mock repo 
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(
            new Product[] {
               new Product {ProductID = 1, Name="P1" },
               new Product {ProductID = 3, Name="P3" },
               product
            }
         );

         AdminController target = new AdminController(mock.Object);
         // Act 
         target.Delete(product.ProductID);

         // Assert - Ensure that the repo method delete was called with the proper argument 
         mock.Verify(m => m.DeleteProduct(product.ProductID));
      }

   }
}
