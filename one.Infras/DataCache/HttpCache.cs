using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;


namespace one.Infras.DataCache
{
    public enum SecondaryCategory
    {
        Authrization,
        BusinessOrder
    }


    public class HttpCacheConfiguration {

        #region
        public const string CachePrefixWord = "[DBCache]";
        public const string None = "";
        public const string AuthorizationTable = "DefaultCache:Auth_UserRole;DefaultCache:Auth_UserRole;DefaultCache:Auth_RoleFunction;DefaultCache:Auth_Function";
        /// <summary>
        /// kendo 分页功能更新的参数 VaryByParam
        /// </summary>
        public const string GridVaryByParam = "sort;page;pageSize;group;filter";


        #endregion


    
    }





    //SecondaryCache
    public class HttpCache
    {

        private static int CacheDuration = 3600;
        private static readonly object Lock = new object();











        











        /// <summary>
        /// 泛型委托返回IEnumerable数据集
        /// </summary>
        /// <param name="func"> </param>
        /// <param name="Id"> 无参数参数</param>
        /// <param name="DependTable">查询所依赖的tabe字符串 </param>
        /// <returns></returns>
        /// 
        public static IEnumerable<T> getEntities<T>(
            Func<IEnumerable<T>> func,
            string DependTable = HttpCacheConfiguration.None,
            bool ReadCache = true)
        {

            string cachekey = string.Format("{1}-{0}", func.Method.Name, HttpCacheConfiguration.CachePrefixWord);
            
            //var cache = HttpContext.Current.Cache.Get(cachekey);


            var cache = HttpRuntime.Cache.Get(cachekey);

            if (cache != null)
            {
                if (ReadCache)
                    return (IEnumerable<T>)cache; /// readcache = true  返回缓存数据

                HttpRuntime.Cache.Remove(cachekey);  /// readcache = false 清除缓存，读取数据库
            }


            var data = func();

            if (ReadCache)
                InsertCache(cachekey, data, CacheDuration);

            return data;
        }




        public static void InsertCache(string CacheKey, object data, int CacheSecond)
        {
            lock (HttpRuntime.Cache)
            {
                HttpRuntime.Cache.Insert(CacheKey, data, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 60, CacheSecond));

            }

        }


        public static void insertCache(string CacheKey, object m, string CacheDepString, int CacheSecond)
        {

            var sqldep = AggregateCacheDepend(CacheDepString);
            lock (HttpRuntime.Cache)
            {
                HttpRuntime.Cache.Insert(CacheKey, m, sqldep, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 60, 0));

            }

        }



        /// <summary>
        /// 根据通用的sql依赖缓存字符,生成合成的缓存对象
        /// </summary>
        /// <param name="csStr"></param>
        /// <returns></returns>
        public static AggregateCacheDependency AggregateCacheDepend(string csStr)
        {

            if (string.IsNullOrEmpty(csStr)) return null;

            var csStrArr = csStr.Split(';');
            AggregateCacheDependency dependency = new AggregateCacheDependency();
            for (int i = 0; i < csStrArr.Length; i++)
            {
                var dbstr = csStrArr[i].Split(':');

                dependency.Add(new SqlCacheDependency(dbstr[0], dbstr[1]));

            }

            return dependency;
        }


    }

}
