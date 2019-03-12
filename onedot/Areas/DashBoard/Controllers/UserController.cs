using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using one.Infras;
using Microsoft.AspNet.Identity;



namespace one.OneDot.Areas.DashBoard.Controllers
{
    public class UserController : BaseController
    {
        // GET: DashBoard/User
        public ActionResult userProfile()
        {
            return View();
        }




        
        public PartialViewResult ProfileInfo(string name ="xxxxx", int age = 12 , bool married = false) {


            var m  = new Service.UserService().getUserSelfProfile(IdentityExtensions.GetUserId(User.Identity));


            return PartialView("PP/_profile", m);
        }




        [HttpPost,ValidateAntiForgeryToken]
        public PartialViewResult ProfileInfo(one.Service.ViewModels.ViewUserProfile model)
        {

            model.UserId = IdentityExtensions.GetUserId(User.Identity);

            if (!ModelState.IsValid) {

                return PartialView("PP/_profile", model);

            }


            new one.Service.UserService().UpdateUserSelfProfile(model);

            AddMessage(Core.Enums.status.Success);

            //ModelState.AddModelError("Success", "数据修改成功!");

            //ModelState.AddModelError("Error", "数据修改成功!");


            return PartialView("PP/_profile", model);
        }








        //public PartialViewResult StyleSafety()
        //{



        //    return PartialView("PP/_StyleSafety");
        //}



    }
}