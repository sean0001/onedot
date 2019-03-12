using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using one.Infras;




namespace one.OneDot.Areas.DashBoard.Controllers
{
    public class SysConfigController : BaseController
    {
        // GET: DashBoard/SysConfigu
        public ActionResult Index()
        {
            return View();
        }



        public PartialViewResult GeneralConfig()
        {


            return PartialView("PP/_GeneralConfig");
        }



        public PartialViewResult EmailConfig() {


            return PartialView("PP/_EmailConfig");
        }



        public PartialViewResult CacheConfig()
        {


            return PartialView("PP/_CacheConfig");
        }




    }
}