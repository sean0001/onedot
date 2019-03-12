using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Runtime.Serialization;
using one.Core.Enums;
using System.Web;
using System.IO.Compression;

namespace one.Infras.DataCache
{

    /// <summary>
    /// 用于自定义页面缓存的时间 Duration
    /// </summary>
    public class PageOutputCache : ActionFilterAttribute
    {
        private string _cachedKey;
        //private const string CachePrifixWord = "[one-PageCache]";
        public int Duration { get; set; }

        private string ComposeParam(IDictionary<string,object> param) {

            StringBuilder sb = new StringBuilder();

            sb.Append("?");
            foreach (var item in param)
            {
                sb.Append(item.Key);
                sb.Append("=");
                sb.Append(item.Value.ToString());
                sb.Append(",");
            }

            return sb.ToString().TrimEnd(',');
        }




        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Url != null)
            {
                var path = filterContext.HttpContext.Request.Url.LocalPath;
                var attributeNames = filterContext.ActionParameters;  // as AttributeNames;
                if (attributeNames != null) _cachedKey = CacheCategory.OPC + "-["+path + ComposeParam(attributeNames)+"]";
            }
            if (filterContext.HttpContext.Cache[_cachedKey] != null)
            {
                
                filterContext.Result = (ActionResult)filterContext.HttpContext.Cache[_cachedKey];
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }





        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Cache.Add(_cachedKey, 
                filterContext.Result, 
                null,
                DateTime.Now.AddSeconds(Duration), 
                System.Web.Caching.Cache.NoSlidingExpiration,
                System.Web.Caching.CacheItemPriority.Default, 
                null);
            base.OnActionExecuted(filterContext);

        }






        #region  暂时无用


        private void Compress(ActionExecutingContext filterContext) {

            HttpRequestBase request = filterContext.HttpContext.Request;

            string acceptEncoding = request.Headers["Accept-Encoding"];

            if (string.IsNullOrEmpty(acceptEncoding)) return;

            acceptEncoding = acceptEncoding.ToUpperInvariant();

            HttpResponseBase response = filterContext.HttpContext.Response;

            if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }




        #endregion



    }
}
