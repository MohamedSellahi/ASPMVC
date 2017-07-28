using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities {
   public class Cart {
      private List<CartLine> _lilneCollection = new List<CartLine>();

      /// <summary>
      /// Gets the linesof the cart as Ienumerable object
      /// </summary>
      /// <value>
      /// The lines.
      /// </value>
      public IEnumerable<CartLine> Lines { get { return _lilneCollection; }}

      public void AddItem(Product product, int quantity) {
         // see if the product exists in the cart 
         // if it exists increment by quantity, otherwise add product to cart 
         CartLine line = _lilneCollection
                        .Where(p => p.Product.ProductID == product.ProductID)
                        .FirstOrDefault();
         if (line == null) {
            _lilneCollection.Add(new CartLine { Product = product, Quantity = quantity });
         }
         else {
            line.Quantity += quantity;
         }
      }

      public void RemoveLine(Product product) {
         _lilneCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
      }

      public decimal ComputeTotalValue() {
         return _lilneCollection.Sum(e => e.Product.Price * e.Quantity);
      }


      public void Clear() {
         _lilneCollection.Clear();
      }

      


   }


   public class CartLine {
      // a class representing one line in the cart list
      public Product Product { get; set; }
      public int Quantity { get; set; }
   }


}
