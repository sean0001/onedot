using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using one.Core;




namespace HttpCheck 
{


    public class SecurityCheck : IHttpModule
    {

          public void Dispose() { }


        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(Application_BeginRequest);
            context.EndRequest += new EventHandler(Application_EndRequest);
        }



        public void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            HttpContext context = application.Context;
            HttpResponse response = context.Response;

            System.Collections.Specialized.NameValueCollection myCol = new System.Collections.Specialized.NameValueCollection();

            string error = string.Empty;
            

            if (application.Request.HttpMethod == "POST")
            {
                //try
                //{
                //error = application.Request.Form.ToString();
                //}
                //catch (HttpRequestValidationException hve)
                //{
                //    Logger.Operation("提交参数：" + Uri.UnescapeDataString(error));
                //}
            // Logger.Operation("提交参数：" + Uri.UnescapeDataString(error));
            // response.Write("这是来自自定义HttpModule中有BeginRequest");

            }

        }



        public void Application_EndRequest(object sender, EventArgs e)
        {
            //HttpApplication application = sender as HttpApplication;
            //HttpContext context = application.Context;
            //HttpResponse response = context.Response;
           // response.Write("这是来自自定义HttpModule中有EndRequest");
        }



    }
}
