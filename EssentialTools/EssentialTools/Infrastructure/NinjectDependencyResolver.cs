using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EssentialTools.Models;
using Ninject;
using Ninject.Web.Common;

using System.Web.Mvc;

namespace EssentialTools.Infrastructure {
   public class NinjectDependencyResolver : IDependencyResolver {
      
      /**
       * variable retaining a collection of services (interface implementations)
       * we get an implementation by calling TryGet/GetAll(Typeof Service) 
       */
      private IKernel _kernel;

      public NinjectDependencyResolver(IKernel kernelParam) {
         _kernel = kernelParam;
         AddBindings();
      }

      /**
       * This methods are part of System.mvc namespace; they are called each time MVC framework needs 
       * an instance of a class to service an incomming request 
       * */
      public object GetService(Type serviceType) {
         return _kernel.TryGet(serviceType);
      }
      public IEnumerable<object> GetServices(Type serviceType) {
         return _kernel.GetAll(serviceType);
      }


      // Add bindings here 
      private void AddBindings() {
         // this will create new instances of the LinqValueCalculator each time 
         // a request is raised 
         //_kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
         //_kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InRequestScope();
         _kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InSingletonScope();

         // Binding with params 
         //_kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>();
         //_kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize", 50M);
         _kernel
            .Bind<IDiscountHelper>()
            .To<DefaultDiscountHelper>()
            .WithConstructorArgument("discountParam", 30M);

         // Conditionnal binding: choose which class to implement an interface
         // this will choose the FlexibleDiscount implementation of IDiscount interface 
         // when this later is injected in a LinqValueCalculator 
         _kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().WhenInjectedInto<LinqValueCalculator>();
      }


   }
}