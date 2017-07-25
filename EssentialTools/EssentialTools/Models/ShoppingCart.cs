using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models {
   public class ShoppingCart {

      //private LinqValueCalculator _calc;
      // interface is better 
      private IValueCalculator _calc; 

      public ShoppingCart(IValueCalculator calcParam) {
         _calc = calcParam;
      }

      // property containing the products, added to the shopping cart
      public IEnumerable<Product> Products { get; set; }

      public decimal CalculateProductTotal() {
         return _calc.ValueProducts(Products);
      }


   }
}