using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace one.OneDot.Controllers
{
    public class errorController : Controller
    {
        // GET: error
        public ActionResult httperror()
        {
            return View();
        }


        public ActionResult notfound() {

            return View();
        }



        public ActionResult internalerror() {

            return View();
        
        }

    }
}