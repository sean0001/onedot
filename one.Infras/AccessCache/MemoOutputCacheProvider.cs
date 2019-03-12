using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Configuration;



namespace one.Infras.AccessCache
{
    public class MemoOutputCacheProvider : OutputCacheProvider
    {

        private const string Prifx = "MC_";




        public override object Add(string key, object entry, DateTime utcExpiry)
        {
            // Do the same custom caching as you did in your 
            // CustomMemoryCache object
            var result = HttpContext.Current.Cache.Get(key);

            if (result != null)
            {
                return result;
            }

            HttpResponse.RemoveOutputCacheItem("");

            HttpContext.Current.Cache.Add(Prifx + key, entry, null, utcExpiry,
                System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);

            return entry;
        }

        public override object Get(string key)
        {
            return HttpContext.Current.Cache.Get(Prifx + key);
        }

        public override void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(Prifx + key);
        }

        public override void Set(string key, object entry, DateTime utcExpiry)
        {
            HttpContext.Current.Cache.Add(Prifx + key, entry, null, utcExpiry,
                System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
        }
    }
}
