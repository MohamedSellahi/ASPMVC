using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CarLotMVC.Domain;

namespace CarLotMVC.EF {
   public class AutoLot:DbContext {
      public DbSet<Inventory> Inventory { get; set; }
      //public System.Data.Entity.DbSet<CarLotMVC.Domain.Car> Cars { get; set; }
   }
}