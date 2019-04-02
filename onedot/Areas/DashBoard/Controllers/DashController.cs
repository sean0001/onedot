using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using one.Infras;

namespace one.OneDot.Areas.DashBoard.Controllers
{
    public class DashController : BaseController
    {
        // GET: DashBoard/Dash
        public ActionResult desktop()
        {
            return View();
        }



        public PartialViewResult mainTopPane()
        {


            ///是不是 vs bug，调试的时候，无法识别认证者， name 为null，所以给临时值
            ///
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                ViewBag.UserName = "no user";
                ViewBag.UserRole = new List<string>() { "no role" };
                return PartialView("_mainTopPanel");
            }

            

            ViewBag.UserName = User.Identity.Name;
            //var m = UGO.Helper.SimExtend.SimExtend.GetUserRole(User.Identity.Name);
            ViewBag.UserRole = new List<string>() { "admin" };
            return PartialView("_mainTopPanel");
        }



        public PartialViewResult mainMenu()
        {


            return PartialView("_mainMenu");
        }



    }
}