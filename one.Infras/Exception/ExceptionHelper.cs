using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using one.Core;

namespace one.Infras
{

    
    public static class ExceptionHelper
    {
        public const string STRING_ENTER_LINEFEED = "\n\r";
        public static System.Exception GetOriginalException(this System.Exception ex)
        {
            if (ex.InnerException == null) return ex;

            return ex.InnerException.GetOriginalException();
        }



        public static void SaveExceptionToLogger(this Exception ex) {

            var message = GetOriginalException(ex).Message;
            Logger.Error(message, ex);
        }


      

        public static void SaveExceptionDetails(this Exception exception)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();


            var properties = exception.GetType().GetProperties();
            var fields = properties.Select(property => new
                             {
                                 Name = property.Name,
                                 Value = property.GetValue(exception, null)
                             })
                             .Select(x => String.Format(
                                 "{0} = {1}",
                                 x.Name,
                                 x.Value != null ? x.Value.ToString() : String.Empty
                             ));

            sb.Append(exception.GetBaseException().Message);
            sb.Append(String.Join(STRING_ENTER_LINEFEED, fields));

            Logger.Error(sb.ToString());
        }












    }





}
