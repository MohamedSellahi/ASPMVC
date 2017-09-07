using System;
using System.ComponentModel.DataAnnotations;
using ModelValidation.Infrastructure;

namespace ModelValidation.Models {

   [NoJoeOnMondays]
   public class Appointment {

      [Required]
      public string ClientName { get; set; }

      [DataType(DataType.DateTime)]
      //[Required(ErrorMessage ="Please enter a date")]
      [FutureDate(ErrorMessage ="Please enter a date in the future")]
      public DateTime Date { get; set; }

      //[Range(typeof(bool),"true", "true",ErrorMessage ="You must accept the termes")]
      [MustBeTrue(ErrorMessage ="You must accept the terms")]
      [UIHint("Boolean")]
      public bool TermsAccepted { get; set; }

      //[UIHint("Boolean2")]
      //public bool TermsAccepted2 { get; set; }
   }
}