using System.Web.Mvc;

namespace one.OneDot.Areas.AppApi
{
    public class AppApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AppApi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AppApi_default",
                "AppApi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}