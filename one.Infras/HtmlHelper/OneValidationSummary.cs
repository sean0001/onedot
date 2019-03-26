using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Routing;




namespace System.Web.Mvc

{
    public static class CostomValidationSummary
    {
        

        public static HtmlString OneValidationSummary(this System.Web.Mvc.HtmlHelper html, bool showTip = false)
        {

            if (html.ViewData.ModelState.IsValid)
                return null;

            StringBuilder sb = new StringBuilder();

            sb.Append("<div class=\"one-validation-summary\">");
            sb.Append("<div class=\"\">");
            sb.Append("<ul>");

            foreach (var state in html.ViewData.ModelState)
            {
                foreach (var mess in state.Value.Errors)
                {

                    if (state.Key == "Success")
                    {
                        
                       sb.AppendFormat("<li class='text-success'> <i class='icon-ok'></i><span>{0}</span></li>", mess.ErrorMessage);
                        //sb.AppendFormat("<li class='Success'> <i class='icon-ok'></i><span>{0}</span></li>", mess.ErrorMessage);
                        sb.Append(one.Core.Utilities.ShowSuccess);
                    }
                    else {
                        
                        sb.AppendFormat("<li class='text-danger'> <i class='icon-exclamation-sign'></i><span>{0}</span></li>", mess.ErrorMessage);
                        //sb.AppendFormat("<li class='Error'> <i class='icon-exclamation-sign'></i><span>{0}</span></li>", mess.ErrorMessage);
                        sb.Append(one.Core.Utilities.ShowError);
                    }
                }
            }

            sb.Append("</ul>");
            sb.Append("</div>");
            sb.Append("</div>");

            return new HtmlString(sb.ToString());

        }

    }
}
