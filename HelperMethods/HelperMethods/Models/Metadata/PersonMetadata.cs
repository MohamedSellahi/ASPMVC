using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Models {
   /// <summary>
   /// Buddy Class for the Proson momdel class to provide data annotation for editor and dispaly htlml helpers
   /// </summary>
   [DisplayName("New Person From Metadata")]
   public partial class PersonMetadata {

      [HiddenInput(DisplayValue = false)]
      public int PersonId { get; set; }

      [Display(Name = "First")]
      [UIHint("MultilineText")]
      public string FirstName { get; set; }

      [Display(Name = "Last")]
      public string LastName { get; set; }

      [Display(Name = "Birth Date")]
      [DataType(DataType.Date)]
      public DateTime BirthDate { get; set; }

      public Address HomeAddress { get; set; }

      [Display(Name = "Approved")]
      [UIHint("Boolean")]
      public bool IsApproved { get; set; }

      [UIHint("Enum")]
      public Role Role { get; set; }
   }
}