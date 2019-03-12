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
    public static partial class UserComponent
    {


        public static HtmlString PanelBlock(this HtmlHelper htmlHelper)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='one-panel-block'><div><script>alert(\'sss\')</script>333333333333333</div></div>");
            return new HtmlString(sb.ToString());
        }



    }
}
