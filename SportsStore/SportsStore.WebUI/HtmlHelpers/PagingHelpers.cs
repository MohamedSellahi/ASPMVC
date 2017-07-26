using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.HtmlHelpers {

   /// <summary>
   /// Contains extension methods for generating special needs html markup 
   /// </summary>
   public static class PagingHelpers {
      /// <summary>
      /// Pages the links.
      /// </summary>
      /// Creates n items of link tags to specific in order to create a pagination
      /// system 
      /// <param name="html">The HTML.</param>
      /// <param name="pageInfo">The page information.</param>
      /// <param name="pageUrl">The page URL.</param>
      /// <returns></returns>
      public static MvcHtmlString PageLinks(this HtmlHelper html, 
                                            PagingInfo pageInfo,
                                            Func<int, string> pageUrl) {
         StringBuilder result = new StringBuilder();
         for (int i = 1; i <= pageInfo.TotalPages; i++) {
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(i));
            tag.InnerHtml = i.ToString();

            if (i== pageInfo.CurrentPage) {
               tag.AddCssClass("selected");
               tag.AddCssClass("btn-primary");
            }
            tag.AddCssClass("btn btn-default");
            result.Append(tag.ToString());
         }
         return MvcHtmlString.Create(result.ToString());
      }



   }
}