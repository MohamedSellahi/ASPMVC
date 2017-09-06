using System;
using System.ComponentModel.DataAnnotations;

namespace ModelValidation.Models {
   public class Appointment {
      public string ClientName { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime Date { get; set; }

      [UIHint("Boolean")]
      public bool TermsAccepted { get; set; }

      //[UIHint("Boolean2")]
      //public bool TermsAccepted2 { get; set; }
   }
}