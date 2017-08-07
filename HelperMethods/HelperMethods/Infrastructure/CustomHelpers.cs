using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Infrastructure {

   // Extension methods for custom html helpers 
   public static class CustomHelpers {

      public static MvcHtmlString ListArrayItems(this HtmlHelper html, string[] items) {
         
         TagBuilder tag = new TagBuilder("ul");

         foreach (string str in items) {
            TagBuilder itemTag = new TagBuilder("li");
            itemTag.SetInnerText(str);
            tag.InnerHtml += itemTag.ToString();
         }
         return new MvcHtmlString(tag.ToString());
      }

      // Force razor to incode string
      // also a problem if we want to generate html tags 
      public static MvcHtmlString DisplayMessage(this HtmlHelper html, string msg) {
         // encoding any data string before sending to the view
         string encodedMsg = html.Encode(msg);
         return new MvcHtmlString(string.Format("This is the message: <p>{0}</p>", encodedMsg));
      }



   }
}