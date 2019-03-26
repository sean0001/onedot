using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    public static class PageClassHelper
    {
            public static string PageClass(this System.Web.Mvc.HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

    }
}