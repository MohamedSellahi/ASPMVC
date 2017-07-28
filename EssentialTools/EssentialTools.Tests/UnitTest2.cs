﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Linq;
using Moq;

namespace EssentialTools.Tests {
   [TestClass]
   public class UnitTest2 {

      private Product[] products = {
         new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
         new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
         new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
         new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
      };


      [TestMethod]
      public void Sum_Product_Correctly() {
         // arrange 
         //var discounter = new MinumiumDiscountHelper();
         //var target = new LinqValueCalculator(discounter);
         //var goalTotal = products.Sum(p => p.Price);


         // create strongly typed object:: it can be any type, it only must implement 
         // the interface 
         Mock<IDiscountHelper> mok = new Mock<IDiscountHelper>();

         // define how the object should behave, so to isolate a single behaviour 
         // and keep focused on the unit test 
         mok.Setup(m => m.ApplayDiscount(It.IsAny<decimal>()))
            .Returns<decimal>(total => total);

         var target = new LinqValueCalculator(mok.Object);

         // act 
         var result = target.ValueProducts(products);

         // Assert
         Assert.AreEqual(products.Sum(p=>p.Price), result,"Sum of products is incorrect");
      }

      private Product[] createProduct(decimal value) {
         return new[] { new Product { Price = value } };
      }

      [TestMethod]
      [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
      public void Pass_Through_Viariable_Discounts() {
         // Arrange 
         Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
         mock.Setup(m => m.ApplayDiscount(It.IsAny<decimal>()))
            .Returns<decimal>(total => total);
         mock.Setup(m => m.ApplayDiscount(It.Is<decimal>(v => v == 0)))
            .Throws<System.ArgumentOutOfRangeException>();
         mock.Setup(m => m.ApplayDiscount(It.Is<decimal>(v => v > 100)))
            .Returns<decimal>(total => (total * 0.9M));
         mock.Setup(m => m.ApplayDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive)))
            .Returns<decimal>(total => (total - 5));

         mock.Setup(m => m.ApplayDiscount(It.Is<decimal>(v => v >= 10 && v <= 100)))
            .Returns<decimal>(total => total - 5);

         var target = new LinqValueCalculator(mock.Object);

         // act 
         decimal FiveDollarDiscount = target.ValueProducts(createProduct(5));
         decimal TenDollarDiscount = target.ValueProducts(createProduct(10));
         decimal FiftyDollarDiscount = target.ValueProducts(createProduct(50));
         decimal HundredDollarDiscount = target.ValueProducts(createProduct(100));
         decimal FiveHundredDollarDiscount = target.ValueProducts(createProduct(500));
         

         // Assert 
         Assert.AreEqual(5, FiveDollarDiscount, "$5 Fails");
         Assert.AreEqual(5, FiveDollarDiscount, "$5 Fail");
         Assert.AreEqual(5, TenDollarDiscount, "$10 Fail");
         Assert.AreEqual(45, FiftyDollarDiscount, "$50 Fail");
         Assert.AreEqual(95, HundredDollarDiscount, "$100 Fail");
         Assert.AreEqual(450, FiveHundredDollarDiscount, "$500 Fail");
         target.ValueProducts(createProduct(0));
      }




   }
}