using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SportsStore.Domain.Entities;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System.Web.Mvc;

namespace SportsStore.UnitTests {
   [TestClass]
   public class CartTests {

      [TestMethod]
      public void Can_Add_New_Lines() {
         // Arrange create some test products 
         Product p1 = new Product { ProductID = 1, Name = "P1" };
         Product p2 = new Product { ProductID = 2, Name = "P2" };

         // Arrange create a new cart
         Cart target = new Cart();

         // act
         target.AddItem(p1, 1);
         target.AddItem(p2, 1);

         CartLine[] result = target.Lines.ToArray();

         //Assert
         Assert.AreEqual(result.Length, 2);
         Assert.AreEqual(result[0].Product, p1);
         Assert.AreEqual(result[1].Product, p2);
      }

      [TestMethod]
      public void Can_Add_Quantity_For_Existing_Lines() {
         // Arrange create some test products 
         Product p1 = new Product { ProductID = 1, Name = "P1" };
         Product p2 = new Product { ProductID = 2, Name = "P2" };

         // Arrange create a cart 
         Cart target = new Cart();

         //Act 
         target.AddItem(p1, 1);
         target.AddItem(p2, 1);
         target.AddItem(p1, 10);

         CartLine[] results = target.Lines.ToArray();

         //Assert
         Assert.AreEqual(results.Length, 2);
         Assert.AreEqual(results[0].Quantity, 11);
         Assert.AreEqual(results[1].Quantity, 1);

      }

      [TestMethod]
      public void Can_Remove_Line() {
         // Arrange - create some test products
         Product p1 = new Product { ProductID = 1, Name = "P1" };
         Product p2 = new Product { ProductID = 2, Name = "P2" };
         Product p3 = new Product { ProductID = 3, Name = "P3" };
         // Arrange - create a new cart
         Cart target = new Cart();
         // Arrange - add some products to the cart
         target.AddItem(p1, 1);
         target.AddItem(p2, 3);
         target.AddItem(p3, 5);
         target.AddItem(p2, 1);

         // Act
         target.RemoveLine(p2);
         // Assert
         Assert.AreEqual(target.Lines.Where(c => c.Product == p2).Count(), 0);
         Assert.AreEqual(target.Lines.Count(), 2);
      }

      [TestMethod]
      public void Calculate_Cart_Total() {
         // Arrange - create some test products
         Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
         Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
         // Arrange - create a new cart
         Cart target = new Cart();
         // Act
         target.AddItem(p1, 1);
         target.AddItem(p2, 1);
         target.AddItem(p1, 3);
         decimal result = target.ComputeTotalValue();
         // Assert
         Assert.AreEqual(result, 450M);
      }

      [TestMethod]
      public void Can_Clear_Contents() {
         // Arrange - create some test products
         Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
         Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
         // Arrange - create a new cart
         Cart target = new Cart();
         // Arrange - add some items
         target.AddItem(p1, 1);
         target.AddItem(p2, 1);
         // Act - reset the cart
         target.Clear();
         // Assert
         Assert.AreEqual(target.Lines.Count(), 0);
      }

      [TestMethod]
      public void Can_Add_To_Cart() {
         // Arrange create mock repo
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category="Apples"}
         }.AsQueryable()
         );

         // create a cart
         Cart cart = new Cart();

         // Arrange create a controller 
         CartController target = new CartController(mock.Object,null);

         // Act 
         target.AddToCart(cart, 1, null);

         //Assert
         Assert.AreEqual(cart.Lines.Count(), 1);
         Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
      }

      [TestMethod]
      public void Addin_Product_to_Cart_Goes_To_Cart_Screen() {
         // Arrange _ create a mock repo
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name="P1", Category="Apples" }
         }.AsQueryable());

         // Arrange - create cart 
         Cart cart = new Cart();

         // Arrange - create a controller 
         CartController target = new CartController(mock.Object, null);

         //Act Add product to the cart
         RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");

         // Assert 
         Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
      }

      [TestMethod]
      public void Can_View_Cart_Contents() {
         // Arrange - create a Cart 
         Cart cart = new Cart();

         // Arrange - create a controller 
         CartController target = new CartController(null, null);

         // act - call the index action method 
         CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

         // Assert
         Assert.AreEqual(result.Cart, cart);
         Assert.AreEqual(result.ReturnUrl, "myUrl");
      }

      [TestMethod]
      public void Cannot_Checkout_Empty_Cart() {
         // Arrange - create mock order processor 
         Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

         // arrange Create an empty cart 
         Cart cart = new Cart();
         ShippingDetails shippingDetails = new ShippingDetails();

         // Arrange create an instance of the controller 
         CartController target = new CartController(null, mock.Object);
         // Act 
         ViewResult result = target.Checkout(cart, shippingDetails);

         //Assert - check that the order hasn't been passed to the processor 
         mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never()
         );

         // Assert - check that themethod is returnin the default view 
         Assert.AreEqual("", result.ViewName);

         //Assert - check im passing an invalid model lto the view 
         Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

      }


      [TestMethod]
      public void Cannot_Check_Invalid_shippingDetails() {
         // Arrange - create a mock order processor 
         Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

         // arrange - create a cart with an item 
         Cart cart = new Cart();
         cart.AddItem(new Product(), 1);

         // Arrange - creat an instance of the controller 
         CartController target = new CartController(null, mock.Object);

         //Arrange - add an error to the model 
         target.ModelState.AddModelError("error", "error");

         // act - try to check out 
         ViewResult result = target.Checkout(cart, new ShippingDetails());

         //Assert - check that the order hasn't been passed on the processor 
         mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

         // Assert - check that the medthod is returning the default view 
         Assert.AreEqual("", result.ViewName);
         //Assert - chet that i m passing an invalid model to the view 
         Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

      }


      [TestMethod]
      public void Can_Checkout_And_Submit_Order() {
         //arrange - create
         Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
         Cart cart = new Cart();
         cart.AddItem(new Product(), 1);
         CartController target = new CartController(null, mock.Object);

         // Act - try checkout 
         ViewResult result = target.Checkout(cart, new ShippingDetails());

         // Assert - check the prder has been passed to the processor 
         mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());

         // Assert - chek the method is returning completed view 
         Assert.AreEqual("Completed", result.ViewName);

         // Assert - check that the method is returning a valid model to the view 
         Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
      }
   }
}
