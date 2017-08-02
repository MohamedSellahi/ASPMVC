using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;


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
         // this is a moq implementation of a repository 
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(
            new List<Product> {
               new Product {Name = "Footbal", Price = 25 },
               new Product {Name = "Surf board", Price = 179},
               new Product {Name = "Running shoes", Price = 95}
            }
            );
         //_kernel.Bind<IProductRepository>().ToConstant(mock.Object);
         _kernel.Bind<IProductRepository>().To<EFProductRepository>();

         // register email service 
         EmailSettings emailSettings = new EmailSettings {
            WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteFile"] ?? "false")
         };

         _kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
         _kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();

      }


   }
}