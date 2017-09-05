using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcModels.Infrastructre;

namespace MvcModels.Models {
   // specify the properties on which we allow binding 
   //[Bind(Include = "City")]

   [ModelBinder(typeof(AddressSummaryBinder))]
   public class AddressSummary {
      public string City { get; set; }
      public string Country { get; set; }
   }
}