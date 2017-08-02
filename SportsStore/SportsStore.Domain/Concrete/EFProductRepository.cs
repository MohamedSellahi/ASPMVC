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

      public Product DeleteProduct(int productID) {
         Product dbEntry = _context.Products.Find(productID);
         if (dbEntry != null) {
            _context.Products.Remove(dbEntry);
            _context.SaveChanges();
         }
         return dbEntry;
      }

      public void SaveProduct(Product product) {
         // this will handle newly created items where the default 
         // value ProducID = 0;
         if (product.ProductID == 0) {
            _context.Products.Add(product);
         }
         else {
            Product dbEntry = _context.Products.Find(product.ProductID);
            if (dbEntry != null) {
               dbEntry.Name = product.Name;
               dbEntry.Description = product.Description;
               dbEntry.Price = product.Price;
               dbEntry.Category = product.Category;
               // add image data 
               dbEntry.ImageData = product.ImageData;
               dbEntry.ImageMimeType = product.ImageMimeType;
            }
         }

         _context.SaveChanges();
      }

      
   }
}
