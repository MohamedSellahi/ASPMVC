using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ClientFeatures {
   public class BundleConfig {
      public static void RegisterBundles(BundleCollection bundles) {
         bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/*.css"));

         bundles.Add(new ScriptBundle("~/bundles/clientfeaturesscripts")
            .Include("~/scripts/jquery-{version}.js",
               "~/scripts/jquery.validate.js",
               "~/scripts/jquery.validate.unobtrusive.js",
               "~/scripts/jquery.unobtrusive-ajax.js")
            );
      }
   }
}