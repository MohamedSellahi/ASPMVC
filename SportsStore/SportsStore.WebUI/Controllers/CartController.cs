using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers {
   public class CartController : Controller {

      private IProductRepository _repository;

      public CartController(IProductRepository repo) {
         _repository = repo;
      }

      public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl) {
         Product prod = _repository.Products.FirstOrDefault(p => p.ProductID == productId);

         if (prod != null) {
            //GetCart().AddItem(prod, 1);
            cart.AddItem(prod, 1);
         }
         return RedirectToAction("Index", new { returnUrl });
      }

      public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl) {

         // is this a valid product id 
         Product prod = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
         if (prod != null) {
            //GetCart().RemoveLine(prod);
            cart.RemoveLine(prod);
         }
         return RedirectToAction("Index", new { returnUrl });
      }

      // this method we=ill be replaced by a model binder 
      private Cart GetCart() {
         Cart cart = (Cart)Session["Cart"];
         if(cart == null) {
            cart = new Cart();
            Session["Cart"] = cart;
         }
         return cart;
      }

      //// GET: Cart
      public ActionResult Index(Cart cart, string returnUrl) {
         return View(new CartViewModel {
            //Cart = GetCart(),
            Cart = cart, // provided by the model binder 
            ReturnUrl = returnUrl
         });
      }
   }
}