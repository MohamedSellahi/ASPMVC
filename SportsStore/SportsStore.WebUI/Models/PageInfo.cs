using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models {

   public class PagingInfo {
      public int TotalItems { get; set; }
      public int itemsPerPage { get; set; }
      public int CurrentPage { get; set; }

      /// <summary>
      /// Gets the total pages.
      /// </summary>
      /// Gets the number of pages needed to display the items in the repository 
      /// <value>
      /// The total pages.
      /// </value>
      public int TotalPages {
         get {
            return (int)Math.Ceiling((decimal)TotalItems / itemsPerPage);
         }
      }



   }
}