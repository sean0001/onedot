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
        public ViewResult httperror(HandleErrorInfo exception)
        {
            return View(exception);
        }


        public ViewResult notfound(HandleErrorInfo exception) {

            return View(exception);
        }



        public ViewResult internalerror(HandleErrorInfo exception) {

            return View(exception);
        
        }

    }
}