using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarLotMVC.Domain;
using CarLotMVC.EF;

namespace CarLotMVC.Controllers {
   public class InventoryController : Controller {
      private AutoLot db = new AutoLot();

      // GET: Inventory
      public ActionResult Index() {
         return View(db.Inventory.ToList());
      }

      // GET: Inventory/Details/5
      public ActionResult Details(int? id) {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Inventory car = db.Inventory.Find(id);
         if (car == null) {
            return HttpNotFound();
         }
         return View(car);
      }

      // GET: Inventory/Create
      public ActionResult Create() {
         return View();
      }

      // POST: Inventory/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "CarID,Color,Maker,PetName")] Inventory car) {
         if (ModelState.IsValid) {
            db.Inventory.Add(car);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         return View(car);
      }

      // GET: Inventory/Edit/5
      public ActionResult Edit(int? id) {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Inventory car = db.Inventory.Find(id);
         if (car == null) {
            return HttpNotFound();
         }
         return View(car);
      }

      // POST: Inventory/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "CarID,Color,Maker,PetName")] Inventory car) {
         if (ModelState.IsValid) {
            db.Entry(car).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(car);
      }

      // GET: Inventory/Delete/5
      public ActionResult Delete(int? id) {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Inventory car = db.Inventory.Find(id);
         if (car == null) {
            return HttpNotFound();
         }
         return View(car);
      }

      // POST: Inventory/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id) {
         Inventory car = db.Inventory.Find(id);
         db.Inventory.Remove(car);
         db.SaveChanges();
         return RedirectToAction("Index");
      }

      protected override void Dispose(bool disposing) {
         if (disposing) {
            db.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
