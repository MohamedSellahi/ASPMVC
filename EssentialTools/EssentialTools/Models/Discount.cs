using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models {

   public interface IDiscountHelper {
      decimal ApplayDiscount(decimal totalParam);
   }

   public class DefaultDiscountHelper : IDiscountHelper {
      public decimal DiscountSize { get; set; }

      public DefaultDiscountHelper(decimal discountParam) {
         DiscountSize = discountParam;
      }

      public decimal ApplayDiscount(decimal totalParam) {
         return (totalParam - (DiscountSize / 100m * totalParam));
      }

   }
}