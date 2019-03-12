using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using one.Infras;
using one.Infras.AccessCache;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using one.Service;
using System.Threading.Tasks;


namespace one.OneDot.Areas.DashBoard.Controllers
{
    public class ManagerController : BaseController
    {

        // GET: DashBoard/Manager


        #region 角色功能操作

        public ActionResult RoleFunc()
        {

            // new PowerService().GetUserMenuTree()
           var ss = new PowerService();
            var mm = ss.GetTree();
            ViewBag.Tree = mm;
            return View();

        }




        public JsonResult read_ModelTreeRoles(string RoleId, int moduleId = 0)
        {

            var model = new PowerService().MenuTree
                .Where(a => a.ModuleId == moduleId);

            return Json(model,JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 更新角色功能
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public JsonResult update_ModelTreeRoles(string RoleId, string[] chked , int moduleId = 0 )
        {

            new PowerService().UpdateRolePrivilege(moduleId, RoleId, chked);

            var mm = ModelState.ToDataSourceResult();

            return Json(mm, JsonRequestBehavior.AllowGet);

        }






        public PartialViewResult getMenuTree(string RoleId,int moduleId = 0) {


            ///测试
            ///
            var model = new PowerService().GetRolesModuleMenuTree(RoleId, moduleId);

            ///
            ///var model = new PowerService().MenuTree.Where(a => a.ModuleId == moduleId);

            return PartialView("PP/_menuTree", model);
        }





        public PartialViewResult RolesEdit() {



            return PartialView("pp/_RolesEdit");

        }

        public JsonResult read_Roles([DataSourceRequest] DataSourceRequest request)
        {

            var model = new RoleService().GetRoles();

            return Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }



        public JsonResult update_roles([DataSourceRequest] DataSourceRequest request, ViewRole vrole) {

            new RoleService().UpdateRole(ref vrole);


            return Json(new[] { vrole }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        public JsonResult Destroy_roles([DataSourceRequest] DataSourceRequest request, ViewRole vrole)
        {

            new RoleService().DestoryRole(ref vrole);


            return Json(new[] { vrole }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion



        #region 用户角色操作


        public ActionResult UserRole() {

            ViewBag.Roles = new UserRoleService().GetRolesAll();



            return View();
        }



        public JsonResult read_UserAndRoles([DataSourceRequest] DataSourceRequest request)
        {

            var model = new UserRoleService().GetUserAndRoles();

            return Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }






        public ActionResult update_UserAndRoles([DataSourceRequest] DataSourceRequest request, ViewRolesOfUser vrou)
        {

            //if(!ModelState.IsValid){

            //}

            //var d = new baiyu.Models.DAL.system_Parm();
            //TryUpdateModel(d);

            //if(!new finanService().updateConfigData(d)){
            //    ModelState.AddModelError(GetCssType(ClientAjaxPromaptCss.errorcss),
            //       getResStr("Client_Prompt_SaveError"));
            //return Json(new[] { scd }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            //}



            new UserRoleService().updateRoleForUser(vrou.UserId, vrou.Roles == null ? null : vrou.Roles.Select(s => s.Value).ToArray());

            //ModelState.AddModelError("testerror", "测试ajax 错误");


            return Json(new[] { vrou }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);

        }









        #endregion




        #region 用户管理


        [Route("user/aa", Name = "GetUserById")]
        [one.Infras.DataCache.PageOutputCache(Duration = 6000)]
        public ActionResult UserInfo() {



            return View();
        }



        
        public JsonResult read_UserInfo([DataSourceRequest] DataSourceRequest request)
        {
            var mmm = Request.Url;

            var model = new UserService().GetAllUsers();

            return Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }


        
        public ActionResult update_UserInfo([DataSourceRequest] DataSourceRequest request, ViewUser viewuser)
        {

            
            
            new UserService().updateUserInfo(ref viewuser);

            //ModelState.AddModelError("错误", "错误信息显示1");
            //ModelState.AddModelError("警告", "错误信息显示2");
            //ModelState.AddModelError("消息", "错误信息显示3");
            //AddMessage(Core.Enums.status.Error);
            //ModelState.AddModelError(Core.Enums.status.Error.ToString(), "测试ajax 错误测试ajax 错误测试ajax 错误测试ajax 错误测试ajax 错误测试ajax 错误测试ajax 错误测试ajax 错误");

            return Json(new[] { viewuser }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);

        }





        public ActionResult destroy_UserInfo([DataSourceRequest] DataSourceRequest request, ViewUser viewuser)
        {


            new UserService().destroyUserInfo(viewuser);

            //ModelState.AddModelError("testerror", "测试ajax 错误");


            return Json(new[] { viewuser }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);

        }


        #endregion





        #region 系统菜单功能设置

        [OutputCache(Duration = 10)]
        public ActionResult sysmenu() {

            
            

            return View();
        }

      



        public JsonResult read_menu([DataSourceRequest] DataSourceRequest request)
        {

            var model = new PowerService().GetTreeList();

            return Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }





        public ActionResult update_menu([DataSourceRequest] DataSourceRequest request, MenuTree mt)
        {

            new PowerService().UpdateFuncTree(ref mt);

            return Json(new[] { mt }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);

        }



        #endregion






    }


}