using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Models {
   public class ProductsListViewModel {

      public IEnumerable<Product> Products { get; set; }
      public PagingInfo PaginInfo { get; set; }

      // define which category to be listed in a view 
      public string CurrentCategory { get; set;}

   }
}