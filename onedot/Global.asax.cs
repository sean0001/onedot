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
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }



        protected void Application_Error(object sender, EventArgs e)
        {
            if (Context.IsCustomErrorEnabled)
                ShowCustomErrorPage(sender, Server.GetLastError());
        }







        protected void Application_BeginRequest(object sender, EventArgs e)
        {


            HttpCookie cookie;
            System.Globalization.CultureInfo culture;
            if (HttpContext.Current.Request.Cookies["lang"] != null)
            {
                cookie = HttpContext.Current.Request.Cookies["lang"];
            }
            else
            {
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






        private void ShowCustomErrorPage(object sender, Exception exception)
        {
            HttpException httpException = exception as HttpException;


            var httpContext = ((MvcApplication)sender).Context;
            var currentController = " ";
            var currentAction = " ";
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            if (currentRouteData != null)
            {
                if (currentRouteData.Values["controller"] != null && !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                }

                if (currentRouteData.Values["action"] != null && !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }





            if (httpException == null)
                httpException = new HttpException(500, "Internal Server Error", exception);
            Response.Clear();
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "error");
            routeData.Values.Add("fromAppErrorEvent", true);

            routeData.Values.Add("exception", new HandleErrorInfo(httpException, currentController, currentAction));

            switch (httpException.GetHttpCode())
            {
                case 403:
                    routeData.Values.Add("action", "httperror");
                    break;

                case 404:
                    routeData.Values.Add("action", "notfound");
                    break;

                case 500:
                    routeData.Values.Add("action", "internalerror");
                    break;
                default:
                    routeData.Values.Add("action", "OtherHttpStatusCode");
                    routeData.Values.Add("httpStatusCode", httpException.GetHttpCode());
                    break;

            }

            Server.ClearError();
            IController controller = new one.OneDot.Controllers.errorController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}
