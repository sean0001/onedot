using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using one.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;


namespace one.Infras 
{

    //public class DataSourceResult
    //{
    //    public IEnumerable Data { get; set; }
    //    public object Errors { get; set; }
    //    public int Total { get; set; }
    //}



    public class error {
      public Dictionary<string, Object> Errors { get; set; }

    }


    


    public class OneHandleErrorAttribute : HandleErrorAttribute
    {

        private object getError(ExceptionContext Context)
        {
            var mess = Context.Exception.GetOriginalException().Message ?? "";
            var _error = new Dictionary<string, object>();
            var te = new Dictionary<string, Object>();
            te.Add("Errors", mess);
            _error.Add("error", te);
            return _error;
        }



        public override void OnException(ExceptionContext Context)
        {


            if (Context.HttpContext.Request.IsAjaxRequest())
            {
                Context.Result = new JsonResult
                {
                    Data = new DataSourceResult()
                    {
                        Data = null,
                        Errors = getError(Context),
                        Total = 0,
                        AggregateResults = null
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                base.OnException(Context);
                if (!Context.ExceptionHandled)
                    return;
            }

            Context.ExceptionHandled = true;
            Logger.Error(Context.Exception.Message, Context.Exception);
        }










        /*
         public override void OnException(ExceptionContext Context)
         {



             //var controller = ((Controller)Context.Controller);
             //controller.ModelState.AddModelError("", "");

             if (Context.HttpContext.Request.IsAjaxRequest())
             {
                 var mess = Context.Exception.GetOriginalException().Message ?? "";
                 Dictionary<string, string> d = new Dictionary<string, string>();
                 d.Add("error", mess);

                 Context.Result = new JsonResult
                 {
                     Data = new DataSourceResult()
                     {
                         Data = null,
                         Errors = d,
                         Total = 0

                     },
                     JsonRequestBehavior = JsonRequestBehavior.AllowGet
                 };
             }
             else
             {

                 base.OnException(Context);
                 if (!Context.ExceptionHandled)
                     return;

             }
             Context.ExceptionHandled = true;
             Logger.Error(Context.Exception.Message, Context.Exception);
         }

     */

    }




    //public class AjaxOneHandleErrorAttribute : HandleErrorAttribute //FilterAttribute, IExceptionFilter
    //{
    //    public void OnException(ExceptionContext filterContext)
    //    {
    //        if (filterContext.HttpContext.Request.IsAjaxRequest())
    //        {
    //            String err = "Aploogies, your request could not be processed";
    //            filterContext.HttpContext.Response.StatusCode = 500;
    //            var serviceException = filterContext.Exception;
    //            if (serviceException != null)
    //            {
    //                err += serviceException.Message + "\n";
    //            }
    //            ContentResult res = new ContentResult();
    //            JsonResult ress = new JsonResult();
               
    //            res.Content = err;
    //            filterContext.Result = res;
    //            filterContext.ExceptionHandled = true;
    //        }
    //    }
    //}


}
