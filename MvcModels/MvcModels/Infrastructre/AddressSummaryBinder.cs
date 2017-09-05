using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcModels.Models;
using System.Web.Mvc;

namespace MvcModels.Infrastructre {
   public class AddressSummaryBinder : IModelBinder {

      public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
         AddressSummary model = (AddressSummary)bindingContext.Model ??
            new AddressSummary();
         model.City = GetValue(bindingContext, "City");
         model.Country = GetValue(bindingContext, "Country");
         return model;
      }

      private string GetValue(ModelBindingContext bindingContext, string name) {
         // i m waiting for ModelName in the form of "[0]"
         name = (bindingContext.ModelName == "" ? "" : bindingContext.ModelName + ".") + name;

         ValueProviderResult result = bindingContext.ValueProvider.GetValue(name);
         if (result == null || result.AttemptedValue == "") {
            return "<not Specified>";
         }
         else {
            return (string)result.AttemptedValue;
         }
      }
   }
}