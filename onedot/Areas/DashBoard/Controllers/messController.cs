using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using one.Infras;




namespace one.OneDot.Areas.DashBoard.Controllers
{
    public class messController : BaseController
    {

        // GET: DashBoard/mess
        public ActionResult message()
        {


            return View();
        }
    }
}