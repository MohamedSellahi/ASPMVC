using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarLotMVC.Domain {
   public class Inventory {
      [Key]
      public int CarID { get; set; }

      public string Color { get; set; }
      public string Maker { get; set; }
      public string PetName { get; set; }
   }
}