using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using one.Infras;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using one.Infras.DataCache;
using one.OneDot.Areas.Stocks.Repository;



namespace one.OneDot.Areas.Stocks.Controllers
{
    public class analyController :BaseController
    {
        // GET: Stocks/analy


        #region

        public ActionResult test()
        {

            return View();
        }

        #endregion
        




        public ActionResult stockgate() {


            return View();
        }






        public ActionResult hover_Pickup(DateTime? pickUpTime, Decimal? range, int? days , decimal? volumeRate, int? UpOrDown)
        {

            if (pickUpTime == null) {
                ViewBag.pickUpTime = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.range = 0.3;
                ViewBag.days = 5;
                ViewBag.volumeRate = 1.8;
                ViewBag.UpOrDown = 1;
                return View();
            }
            else
            {
                ViewBag.pickUpTime = pickUpTime.GetValueOrDefault().ToString("yyyy-MM-dd");
                ViewBag.range = range;
                ViewBag.days = days;
                ViewBag.volumeRate = volumeRate;
                ViewBag.UpOrDown = UpOrDown;
                return View(HoverPickupRepository.getHoverPickup(pickUpTime.GetValueOrDefault(), range ?? 0, days ?? 0, volumeRate?? 0, ViewBag.UpOrDown??0 ));
            }
                

            

        }


        public JsonResult getStock(string code = "300063") {


            var m = BaseStockRepository.GetSingleDayLine(code);

            return Json(m, JsonRequestBehavior.AllowGet);
            
        }



        public ActionResult dayline()
        {
            var m = BaseStockRepository.GetSingleDayLine("300063");
            return View(m);
        }
    }
}