using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace one.Infras
{
    [Authorize]
    [OneHandleError]
    public class BaseController : Controller
    {

        protected virtual void AddMessage(Core.Enums.status state, string mess = null) {

            if (string.IsNullOrEmpty(mess))
            {
                if(state == Core.Enums.status.Error)
                    ModelState.AddModelError(state.ToString(), one.Res.Lang.OperationFailure);

                if(state == Core.Enums.status.Success)
                    ModelState.AddModelError(state.ToString(), one.Res.Lang.OperationSuccess);
            }
            else {
                ModelState.AddModelError(state.ToString(), mess);
            }

        }




        /// <summary>
        /// 返回已知异常的提示信息
        /// </summary>
        /// <param name="Message"></param>
        //protected virtual void KnownException(string message)
        //{

        //    if (Request.IsAjaxRequest())
        //    {
        //        if (Request.IsAuthenticated)
        //        {

        //        }
        //        else
        //        {
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("__ErrorKey__", message);
        //    }
        //}




    }
}
