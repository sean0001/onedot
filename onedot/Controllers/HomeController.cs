using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using one.Res;
using one.Core;
using Microsoft.AspNet.Identity;
using one.Infras;
using one.OneDot.Repository;

namespace onedot.Controllers
{
    public class HomeController : BaseController
    {



        public JsonResult textajax() {


            throw new Exception("Error Occured !");


            return Json("显示错误信息");
        }


        [AllowAnonymous]
#if !DEBUG
        
       [OutputCache(Duration=10000,VaryByParam="*")]
#endif
        public ActionResult Index(int? param)
        {

           // throw new Exception("Error Occured !");
            if (param == 1) {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpResponse.RemoveOutputCacheItem("/home/index");
                Response.CacheControl = "no-cache";
            
            }

           
            
            //var ss = new one.Service.PowerService();
           // var mm = ss.GetTree();


            ViewBag.Now = DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss：ffff");

            //Response.Cache.SetNoServerCaching();

             //test();

           // var m = new one.Service.PowerService();

           // var bb = m.GetUserMenuTree("ae4bc42b88b254ab");

           // var cc = m.GetUserMenuTree(null);
            
            Logger.Warning("第一次测试");

            return View();
        }


        private void test() {

            //---------------

            string[] srole = new string[] { "2", "3", "4" };

            var ss1 = new one.Service.UserRoleService();

            ss1.DeleteRoleForUser("94525fae7223a3f", srole);

            //---------------------

            var a = new one.Service.UserService();

            var mmmmm = new one.Service.RoleService();
            var bbs = mmmmm.GetById("1");


            //var mmm = a.FuncTree;

            var mm = User.Identity.GetUserId();

            //var m1m = one.Service.RoleService.GetRoleForUser(mm);

            var ss = User.Identity.AuthenticationType;
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        [AllowAnonymous]
        public ActionResult Pricing() {

            ViewBag.Slides = SlideRepository.GetSlide("");

            return View();
        }



    }



    



}