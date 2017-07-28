using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CSharp;

using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;


namespace SportsStore.UnitTests {
   [TestClass]
   public class UnitTest1 {



      [TestMethod]
      public void Can_paginate() {
         // arrange 
         Mock<IProductRepository> mock = new Mock<IProductRepository>();

         mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2" },
            new Product {ProductID = 3, Name = "P3" },
            new Product {ProductID = 4, Name = "P4" },
            new Product {ProductID = 5, Name = "P5" }
         });

         ProductController controller = new ProductController(mock.Object) {
            PageSize = 3
         };

         // arrange 
         ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

         // act 

         //IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;
         // Assert 
         Product[] prodArray = result.Products.ToArray();

         Assert.IsTrue(prodArray.Length == 2);
         Assert.AreEqual(prodArray[0].Name, "P4");
         Assert.AreEqual(prodArray[1].Name, "P5");
      }


      [TestMethod]
      public void Ca_Generate_Page_Links() {
         // Arrange - define an html helper - we need to do this 
         // in order to applay the extension method 
         HtmlHelper myHelper = null;

         // Arrange - Create PageInfo 
         PagingInfo pInfo = new PagingInfo {
            CurrentPage = 2,
            TotalItems = 28,
            itemsPerPage = 10
         };

         // Arrange - set up a delegate using lambda expression 
         Func<int, string> pageUrlDelegate = i => "Page" + i;

         // Act
         MvcHtmlString result = myHelper.PageLinks(pInfo, pageUrlDelegate);

         //assert: Total pages = ceilling(28/10) = 3;
         Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>" +
            @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>" +
            @"<a class=""btn btn-default"" href=""Page3"">3</a>",
            result.ToString());

      }

      [TestMethod]
      public void Can_Send_Pagination_view_Model() {
         // arrange 
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1" },
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
            new Product {ProductID = 4, Name = "P4"},
            new Product {ProductID = 5, Name = "P5"}
         });

         // arrange 
         ProductController controller = new ProductController(mock.Object) {
            PageSize = 3
         };

         // Act 
         ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

         // Assert 
         PagingInfo pageInfo = result.PaginInfo;

         Assert.AreEqual(pageInfo.CurrentPage, 2);
         Assert.AreEqual(pageInfo.itemsPerPage, 3);
         Assert.AreEqual(pageInfo.TotalItems, 5);
         Assert.AreEqual(pageInfo.TotalPages, 2);
      }

      [TestMethod]
      public void Can_Filter_Products() {
         //Arrange
         // create mpck repo 
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
            new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
            new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
            new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
            new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
         });

         // arrange crate controller and make sizepage = 3
         ProductController controller = new ProductController(mock.Object) {
            PageSize = 3
         };

         // Act 
         Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

         // Assert
         Assert.AreEqual(result.Length, 2);
         Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
         Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");

      }

      [TestMethod]
      public void Can_Create_Categories() {
         // Arrange
         // - create a mock repo
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Apples"},
            new Product {ProductID = 2, Name = "P2", Category = "Apples"},
            new Product {ProductID = 3, Name = "P3", Category = "Plums"},
            new Product {ProductID = 4, Name = "P4", Category = "Oranges"}
         });

         // Act 
         NavController target = new NavController(mock.Object);

         // assert 
         string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

         // Assert 
         Assert.AreEqual(results.Length, 3);
         Assert.AreEqual(results[0], "Apples");
         Assert.AreEqual(results[1], "Oranges");
         Assert.AreEqual(results[2], "Plums");
      }

      [TestMethod]
      public void Indicates_Selected_Category() {
         // Arrange
         // - create the mock repository
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[] {
         new Product {ProductID = 1, Name = "P1", Category = "Apples"},
         new Product {ProductID = 4, Name = "P2", Category = "Oranges"},
         });

         NavController target = new NavController(mock.Object);

         // Arrange define the category to be selected 
         string categoryToSelect = "Apples";

         //Action 
         string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

         // Assert
         Assert.AreEqual(categoryToSelect, result);
      }


      [TestMethod]
      public void Generate_category_Specific_Product_Count() {
         // Arrange 
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
            new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
            new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
            new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
            new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
         });

         // Arrange create controller 
         ProductController target = new ProductController(mock.Object) {
            PageSize = 3
         };

         //action 
         int res1 = ((ProductsListViewModel)target.List("Cat1").Model).PaginInfo.TotalItems;
         int res2 = ((ProductsListViewModel)target.List("Cat2").Model).PaginInfo.TotalItems;
         int res3 = ((ProductsListViewModel)target.List("Cat3").Model).PaginInfo.TotalItems;
         int resAll = ((ProductsListViewModel)target.List(null).Model).PaginInfo.TotalItems;

         //Assert
         Assert.AreEqual(res1, 2);
         Assert.AreEqual(res2, 2);
         Assert.AreEqual(res3, 1);
         Assert.AreEqual(resAll, 5);

      }
   }
}
