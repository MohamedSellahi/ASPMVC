using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ModelValidation.Models;

namespace ModelValidation.Infrastructure {
   public class NoJoeOnMondaysAttribute: ValidationAttribute {

      public NoJoeOnMondaysAttribute() {
         ErrorMessage = "Joe cannot book appointment on Mondays";
      }

      public override bool IsValid(object value) {
         Appointment app = value as Appointment;
         if (app == null || string.IsNullOrEmpty(app.ClientName) || app.Date == null) {
            // I dont have a model of the right type to validate, or I don't 
            // have the values for the ClientName and Date properties 
            return true;
         }
         else {
            return !(app.ClientName == "Joe" && app.Date.DayOfWeek == DayOfWeek.Monday);
         }
      }
   }
}