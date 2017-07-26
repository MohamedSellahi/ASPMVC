using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

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
         ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

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
         ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

         // Assert 
         PagingInfo pageInfo = result.PaginInfo;

         Assert.AreEqual(pageInfo.CurrentPage, 2);
         Assert.AreEqual(pageInfo.itemsPerPage, 3);
         Assert.AreEqual(pageInfo.TotalItems, 5);
         Assert.AreEqual(pageInfo.TotalPages, 2);
      }



   }
}
