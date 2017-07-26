using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete {
   /// <summary>
   /// wrapper class for data base connection, represent an repository
   /// </summary>
   /// 
   public class EFProductRepository : IProductRepository {

      private EFDbContext _context = new EFDbContext();
      
      // return the products 
      public IEnumerable<Product> Products {
         get { return _context.Products;}
      }


   }
}
