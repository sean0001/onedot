using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;
using one.Infras;



namespace one.OneDot
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }






        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            
            HttpCookie cookie;
            System.Globalization.CultureInfo culture;
            if (HttpContext.Current.Request.Cookies["lang"] != null)
            {
                cookie = HttpContext.Current.Request.Cookies["lang"];
            }
            else {
                cookie = new HttpCookie("lang", "zh-CN");
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

            string lang = cookie.Value;

            try
            {
                culture = new System.Globalization.CultureInfo(lang);
            }
            catch (Exception ex)
            {
                
                cookie = new HttpCookie("lang", "zh-CN");
                HttpContext.Current.Response.Cookies.Add(cookie);
                culture = new System.Globalization.CultureInfo(cookie.Value);
                ex.SaveExceptionDetails();
            }
            
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }



    }
}
