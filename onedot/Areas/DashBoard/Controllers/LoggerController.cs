using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using one.Infras;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using one.Service;


namespace one.OneDot.Areas.DashBoard.Controllers
{
    public class LoggerController : BaseController
    {
        // GET: DashBoard/Logger
        public ActionResult Index()
        {


            Core.Logger.Operation("one.Core.Logger.Operation");
            Core.Logger.Warning("one.Core.Logger.Infor");
            Core.Logger.Error("one.Core.Logger.Error");

            return View();
        }





        public PartialViewResult LoggerInfo() {


            return PartialView("PP/_LoggerInfo");
        }


        public PartialViewResult LoggerError()
        {


            return PartialView("PP/_LoggerError");
        }



        public PartialViewResult LoggerOperation()
        {

        

            return PartialView("PP/_LoggerOperation");
        }






        public PartialViewResult LoggerContent(string Name,Core.Enums.status status) {

           string content = new LoggerService().ReadLoggerFile(Name, status);

            return PartialView("PP/_LoggerContent", content);
        }







        public ActionResult read_LoggerFiles([DataSourceRequest] DataSourceRequest request, Core.Enums.status status = Core.Enums.status.Info)
        {

            var files = new LoggerService().GetLogFiles(status);

            return Json(files.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult Delete_LoggerFiles(Core.Enums.status status = Core.Enums.status.Info)
        {

            new LoggerService().ClearAllLogger(status);

            AddMessage(Core.Enums.status.Success);
            return Json(ModelState.ToDataSourceResult());

        }



    }
}