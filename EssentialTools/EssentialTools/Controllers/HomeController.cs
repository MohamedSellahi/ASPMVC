using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Controllers {
   public class HomeController : Controller {

      private IValueCalculator _calc;

      public HomeController(IValueCalculator calcParam) {
         _calc = calcParam;
      }

      private Product[] _products = {
         new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
         new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
         new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
         new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
      };


      // GET: Home
      public ActionResult Index() {
         ShoppingCart cart = new ShoppingCart(_calc) { Products = _products };
         decimal totalValue = cart.CalculateProductTotal();
         return View(totalValue);
      }
   }
}


/**
        // need to get rid of this to elliminate tight coupling with LinqValueCalculator
        //LinqValueCalculator calc = new LinqValueCalculator();

        // USe interface, but still a problem since i need to instanciate a LinqValueCalculator
        IValueCalculator calc = new LinqValueCalculator();
        //this will be solved with a Dependency injection framework
        
        

// prepare ninject for use: Object responsible for resolving dependencies
// and creating new objects 
IKernel ninjectKernel = new StandardKernel();

// Configure ninject kernel lto choose an implementation of IValueCalculator
// 
ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
         
         // 
         // Get a reference to an object implementing IValueCalculator (LinqValueCalculatort)
         //IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();
*/