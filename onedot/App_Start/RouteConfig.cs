using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace one.OneDot
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{file}.css");
            routes.IgnoreRoute("{file}.jpg");
            routes.IgnoreRoute("{file}.gif");
            routes.IgnoreRoute("{file}.png");
            routes.IgnoreRoute("{file}.js");
            routes.IgnoreRoute("{file}.php");
            routes.IgnoreRoute("{file}.jsp");



            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Dashboards", action = "Dashboard_1", id = UrlParameter.Optional }
            );

        }
    }
}
