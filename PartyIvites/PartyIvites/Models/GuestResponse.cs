using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;// for Form data validation 

namespace PartyIvites.Models {
   public class GuestResponse {

      [Required(ErrorMessage ="Please enter your name")]
      public string Name { get; set; }

      [Required(ErrorMessage ="Please enter your email address")]
      [RegularExpression(".+\\@.+\\..+",
         ErrorMessage ="Please enter a valid email address")]
      public string Email { get; set; }

      [Required(ErrorMessage ="Please enter a phone number")]
      public string Phone { get; set; }

      [Required(ErrorMessage ="Please specify whether you'll attend")]
      public bool? WillAttend { get; set; }

   }
}