using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace SportsStore.WebUI.Infrastructure {
   public class NinjectDependencyResolver : IDependencyResolver {

      private IKernel _kernel;

      public NinjectDependencyResolver(IKernel kernelparam) {
         _kernel = kernelparam;
         Addbindings();
      }

     
      public object GetService(Type serviceType) {
         return _kernel.TryGet(serviceType);
      }

      public IEnumerable<object> GetServices(Type serviceType) {
         return _kernel.GetAll(serviceType);
      }



      private void Addbindings() {
         // Bindings goes here
      }


   }
}