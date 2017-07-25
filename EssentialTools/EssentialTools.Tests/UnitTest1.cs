using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;



namespace EssentialTools.Tests {
   [TestClass]
   public class UnitTest1 {

      private IDiscountHelper getTestObject() {
         return new MinumiumDiscountHelper();
      }




      [TestMethod]
      public void Discount_above_100() {
         // Arrange 
         IDiscountHelper target = getTestObject();
         decimal total = 200;

         // act 
         var discountedTotal = target.ApplayDiscount(total);

         // assert 
         Assert.AreEqual(total * 0.9M, discountedTotal);
      }

      [TestMethod]
      public void Discount_between_10_And_100() {
         // Arrange
         IDiscountHelper target = getTestObject();

         //act 
         decimal TendollarDiscount = target.ApplayDiscount(10);
         decimal HundredDollarDiscount = target.ApplayDiscount(100);
         decimal FiftyDollarDiscount = target.ApplayDiscount(50);

         //Assert
         Assert.AreEqual(5, TendollarDiscount, "$10 test is wrong");
         Assert.AreEqual(95, HundredDollarDiscount, "$100 test is wrong");
         Assert.AreEqual(45, FiftyDollarDiscount, "$50 test is wrong");
      }


      [TestMethod]
      public void Discount_less_then_10() {
         // arrange
         IDiscountHelper target = getTestObject();

         // act
         decimal discount5 = target.ApplayDiscount(5);
         decimal discount0 = target.ApplayDiscount(0);

         //assert
         Assert.AreEqual(5, discount5);
         Assert.AreEqual(0, discount0);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void Discount_negative_value() {
         // arrange 
         IDiscountHelper target = getTestObject();

         // act
         target.ApplayDiscount(-1);
      }


   }
}
