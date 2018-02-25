using FiltersRevisited.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiltersRevisited.Controllers {
   public class CustomerController : Controller {
      // GET: Customer

      [SimpleMessage(Message = "A")]
      [SimpleMessage(Message = "B")]
      public string Index() {
         return "This is the customer controller";
      }
   }
}