using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using one.Infras;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using one.Infras.DataCache;

namespace one.OneDot.Areas.DashBoard.Controllers
{
    public class CacheManaController : BaseController
    {
        // GET: DashBoard/CacheMana
        public ActionResult Index()
        {

            return View();
        }




        public ActionResult read_cache([DataSourceRequest] DataSourceRequest request, Core.Enums.CacheCategory cacheCategory)
        {
            var data = one.Infras.DataCache.SecondCache.GetSecondCache(cacheCategory);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }




        public ActionResult destroySingleCache([DataSourceRequest] DataSourceRequest request, SecondCacheItemProperty scp)
        {

            Infras.DataCache.SecondCache.DestroySingleSecondCache(scp.Url);

            return Json(new[] { scp }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);

        }





        public ActionResult destroyCache([DataSourceRequest] DataSourceRequest request, Core.Enums.CacheCategory CacheCategory)
        {

            Infras.DataCache.SecondCache.DestroyCacheOfCategory(CacheCategory);

            AddMessage(Core.Enums.status.Success);

            return Json(new[] { CacheCategory }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);

        }





        public PartialViewResult SecondCache()
        {

            return PartialView("PP/_SecondCache");
        }




        public PartialViewResult PageCache()
        {

            return PartialView("PP/_pageCache");
        }



        public PartialViewResult SysCache()
        {

            //ViewBag.CacheCount = HttpRuntime.Cache.Count;
            //ViewBag.CacheSize = HttpRuntime.Cache.EffectivePercentagePhysicalMemoryLimit;
            //ViewBag.CacheS = HttpRuntime.Cache.EffectivePrivateBytesLimit;
            return PartialView("PP/_SysCache");
        }








    }
}