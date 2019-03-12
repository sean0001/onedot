using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Routing;




namespace one.Infras.HtmlHelper
{
  public static class ListBoxHelper
    {


      public static HtmlString PrintSpace(
          this System.Web.Mvc.HtmlHelper htmlHelper, 
          List<SelectListItem> Source = null,
          List<SelectListItem> Target = null
          )
        {
          StringBuilder sb = new StringBuilder();

          sb.Append("<div class='list-box-group'>{0}</div>");

          if (Source != null) {
              foreach (var item in Source)
              {

              }
          }
          



          //for (int i = 0; i < num; i++)
          //{
          //    sb.Append("&nbsp;");
          //}



          return new HtmlString(sb.ToString());

      }



    }
}
