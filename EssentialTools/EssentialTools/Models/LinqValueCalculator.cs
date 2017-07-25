using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models {
   public class LinqValueCalculator : IValueCalculator {

      private IDiscountHelper _discounter;
      private static int counter = 0;

      public LinqValueCalculator(IDiscountHelper discountparam) {
         _discounter = discountparam;

         // for debug purposes, to count the number of instance that wgere calculated 
         System.Diagnostics.Debug.WriteLine(
            string.Format("Instance {0} created", ++counter));
      }

      public decimal ValueProducts(IEnumerable<Product> products) {
         return _discounter.ApplayDiscount(products.Sum(p => p.Price));
      }


   }
}