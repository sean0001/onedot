using System.Web.Mvc;

namespace one.OneDot.Areas.dashBoard
{
    public class dashBoardAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "dashBoard";
            }
        }



        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "dashBoard_default",
                "dashBoard/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}