using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;



namespace SportsStore.WebUI.Controllers {
   public class ProductController : Controller {

      private IProductRepository _repository;
      public int PageSize = 4;

      public ProductController(IProductRepository productRepository) {
         _repository = productRepository;
      }


      // Adds pagination 
      public ViewResult List(string category, int page = 1) {
         // if category == null; we take all the elements 
         ProductsListViewModel model = new ProductsListViewModel {
            Products = _repository.Products.OrderBy(p => p.ProductID)
                                           .Where(p => category == null || p.Category == category)
                                           .Skip((page - 1) * PageSize)
                                           .Take(PageSize),
            PaginInfo = new PagingInfo {
               CurrentPage = page,
               itemsPerPage = PageSize,
               TotalItems = category == null ?  // this is needed to correct thenumber of totalitems in the current category 
               _repository.Products.Count() :
               _repository.Products.Where(e => e.Category == category).Count()
            },
            CurrentCategory = category
         };
         return View(model);
      }

      // get product  image from data base 
      public FileContentResult GetImage(int productId) {
         Product prod = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
         if (prod != null) {
            return File(prod.ImageData, prod.ImageMimeType);
         }
         else {
            return null;
         }
      }


   }
}