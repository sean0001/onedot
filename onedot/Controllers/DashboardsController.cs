﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace one.OneDot.Controllers
{

    [RouteArea("Admin")]
    [RoutePrefix("menu")]
    [Route("{action}")]
    //[RoutePrefix("ss")]
    public class DashboardsController : Controller
    {
        [Route("s1")]
        public ActionResult Dashboard_1()
        {
            return View();
        }
        
        public ActionResult Dashboard_2()
        {
            return View();
        }

        public ActionResult Dashboard_3()
        {
            return View();
        }
        
        public ActionResult Dashboard_4()
        {
            return View();
        }
        
        public ActionResult Dashboard_4_1()
        {
            return View();
        }

        public ActionResult Dashboard_5()
        {
            return View();
        }

    }
}