using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Linq;

namespace SportsStore.WebUI.Controllers {

   [Authorize]
   public class AdminController : Controller {
      private IProductRepository _repository;

      public AdminController(IProductRepository repo) {
         _repository = repo;
      }

      // GET : request 
      public ViewResult Edit(int productId) {
         // find the product 
         Product prod = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
         return View(prod);
      }

      [HttpPost]
      public ActionResult Edit(Product product, HttpPostedFileBase image = null) {
         if (ModelState.IsValid) {
            if (image!=null) {
               product.ImageMimeType = image.ContentType;
               product.ImageData = new byte[image.ContentLength];
               image.InputStream.Read(product.ImageData, 0, image.ContentLength);
            }
            _repository.SaveProduct(product);
            TempData["message"] = string.Format("{0} has been saved", product.Name);
            return RedirectToAction("Index");
         }
         else {
            // there is somthing wrong with data values 
            return View(product);
         }
      }

      public ViewResult Create() {
         return View("Edit", new Product());
      }


      [HttpPost]
      public ActionResult Delete(int productId) {
         Product deletedProduct = _repository.DeleteProduct(productId);
         if (deletedProduct != null) {
            TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
         }
         return RedirectToAction("Index");
      }

      // GET: Admin
      public ViewResult Index() {
         return View(_repository.Products);
      }


   }
}