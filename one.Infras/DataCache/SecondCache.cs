using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Reflection;
using one.Core.Enums;


namespace one.Infras.DataCache
{


    public static class SecondCache
    {
        private static readonly object locker = new object();
        private const int Duration = 1800;


        /// <summary>
        /// 无参数 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T GetCaheEntity<T>(Func<T> func,
            string UniqueKey = null,
            bool clearCache = false)
        {

            string cachekey = string.Format("{0}-{1}-{2}", CacheCategory.OSC, UniqueKey, func.Method.Name);

            var cache = HttpContext.Current.Cache.Get(cachekey);

            if (clearCache && cache != null)
            {
                RemoveCacheObject(cachekey);
            }


            if (cache != null)
            {
                return (T)cache;
            }

            var m = func();

            InsertCacheObject(cachekey, m, Duration);

            return m;



            //if (cache != null)
            //{
            //    if (!clearCache) return (T)cache; /// readcache = true 返回缓存数据
            //}

            //var m = func();
            //if (!clearCache) InsertCacheObject(cachekey, m, Duration);

        }







        /// <summary>
        /// 根据id返回单个实体 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static T GetCaheEntity<T>(Func<int?, T> func,
            string UniqueKey = null,
            int? Number = null,
            bool clearCache = false)
        {

            //string cachekey = string.Format("{2}-{0}-{1}", func.Method.Name, Number, UniqueKey);
            string cachekey = string.Format("{0}-{1}-{2}-{3}", CacheCategory.OSC, UniqueKey, Number, func.Method.Name);


            var cache = HttpContext.Current.Cache.Get(cachekey);

            if (cache != null)
            {
                if (!clearCache) return (T)cache; /// readcache = true 返回缓存数据

                RemoveCacheObject(cachekey);
            }

            var m = func(Number);
            if (clearCache) InsertCacheObject(cachekey, m, Duration);

            return m;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="UniqueKey">传入的缓存唯一Key参数</param>
        /// <param name="where">查询条件参数</param>
        /// <param name="clearCache">是否清除缓存</param>
        /// <returns></returns>
        public static T GetCaheEntity<T>(Func<string, T> func,
            string UniqueKey = null,
            string where = null,
            bool ExtractFormCache = true)
        {


            string cachekey = string.Format("{0}-{1}-{2}-{3}", CacheCategory.OSC, UniqueKey, where, func.Method.Name);

            var cache = HttpContext.Current.Cache.Get(cachekey);

            if (cache != null)
            {
                if (ExtractFormCache) return (T)cache; /// readcache = true 返回缓存数据
                RemoveCacheObject(cachekey);
            }

            var data = func(where);
            InsertCacheObject(cachekey, data, Duration);

            return data;
        }






        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="UniqueKey">传入的缓存唯一Key参数</param>
        /// <param name="where">查询条件参数</param>
        /// <param name="clearCache">是否清除缓存</param>
        /// <returns></returns>
        public static T GetCaheEntity<T>(Func<ApplicationMetaDataCategory, T> func,
            string UniqueKey = null,
            Core.Enums.ApplicationMetaDataCategory adc = ApplicationMetaDataCategory.None,
            bool ExtractFormCache = true)
        {


            string cachekey = string.Format("{0}-{1}-{2}-{3}", CacheCategory.OSC, UniqueKey, adc.ToString(), func.Method.Name);

            var cache = HttpContext.Current.Cache.Get(cachekey);

            if (cache != null)
            {
                if (ExtractFormCache) return (T)cache; /// readcache = true 返回缓存数据
                RemoveCacheObject(cachekey);
            }

            var data = func(adc);

            InsertCacheObject(cachekey, data, Duration);

            return data;
        }












        /// <summary>
        /// 删除缓存数据， 在更新操作时执行 缓存删除操作
        /// </summary>
        /// <param name="id"></param>
        public static void RemoveCacheObject(string CacheKeyWord)
        {

            lock (locker)
            {
                HttpRuntime.Cache.Remove(CacheKeyWord);
            }
        }



        public static void InsertCacheObject(string CacheKey, object data, int CacheSecond)
        {
            lock (locker)
            {
                // HttpRuntime.Cache.Insert(CacheKey, data, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(CacheSecond));
                HttpRuntime.Cache.Insert(
                    CacheKey,
                    data,
                    null,
                    DateTime.UtcNow.AddSeconds(CacheSecond),
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.High,
                    null);

            }

        }






        #region  数据缓存管理

        public static SecondCacheItemProperty GetCacheObjectProperty(string cacheKey)
        {
            //object cacheEntry = HttpRuntime.Cache.GetType()
            //    .GetMethod("Get", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(HttpRuntime.Cache, new object[] { cacheKey, 1 });


            var ss = HttpRuntime.Cache.GetType();
            

            var bb = ss.GetMethod("Get", BindingFlags.Instance | BindingFlags.NonPublic );
            if (bb == null) return null;

            var cacheEntry = bb.Invoke(HttpRuntime.Cache, new object[] { cacheKey, 1 });

            PropertyInfo utcExpiresProperty = cacheEntry.GetType()
                .GetProperty("UtcExpires", BindingFlags.NonPublic | BindingFlags.Instance);

            PropertyInfo UtcCreatedProperty = cacheEntry.GetType()
                   .GetProperty("UtcCreated", BindingFlags.NonPublic | BindingFlags.Instance);

            PropertyInfo UsageBucketProperty = cacheEntry.GetType()
                   .GetProperty("UsageBucket", BindingFlags.NonPublic | BindingFlags.Instance);


            PropertyInfo UtcLastUsageUpdateProperty = cacheEntry.GetType()
                   .GetProperty("UtcLastUsageUpdate", BindingFlags.NonPublic | BindingFlags.Instance);



            return new SecondCacheItemProperty
            {
                Url = cacheKey,
                UtcLastUsageUpdate = ((DateTime)UtcLastUsageUpdateProperty.GetValue(cacheEntry, null)).ToLocalTime(),
                UsageBucket = (byte)UsageBucketProperty.GetValue(cacheEntry, null),
                UtcCreated = ((DateTime)UtcCreatedProperty.GetValue(cacheEntry, null)).ToLocalTime(),
                utcExpires = ((DateTime)utcExpiresProperty.GetValue(cacheEntry, null)).ToLocalTime(),
                TimeSpan = DateDiff(((DateTime)UtcCreatedProperty.GetValue(cacheEntry, null)).ToLocalTime(), ((DateTime)utcExpiresProperty.GetValue(cacheEntry, null)).ToLocalTime())

                //((DateTime)UtcCreatedProperty.GetValue(cacheEntry, null)).ToLocalTime().Subtract((DateTime)utcExpiresProperty.GetValue(cacheEntry, null)).Duration().TotalSeconds
            };

        }


        private static string DateDiff(DateTime t1, DateTime t2)
        {
            StringBuilder sb = new StringBuilder();

            TimeSpan ts = t2.Subtract(t1).Duration();

            if (ts.Days > 0)
            {
                if (ts.Days > 365)
                {
                    sb.Append("--永久--");
                    return sb.ToString();
                }
                else
                {
                    sb.Append(ts.Days.ToString());
                    sb.Append("D");
                }
            }


            if (ts.Hours > 0)
            {
                sb.Append(ts.Hours.ToString());
                sb.Append("h");
            }


            if (ts.Minutes > 0)
            {
                sb.Append(ts.Minutes.ToString());
                sb.Append("\"");
            }


            if (ts.Seconds > 0)
            {
                sb.Append(ts.Seconds.ToString());
                sb.Append("\'");
            }

            return sb.ToString();

        }



        public static void DestroyCacheOfCategory(CacheCategory cc)
        {

            string key = cc.ToString() + "-";

            System.Collections.IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var name = (string)enumerator.Key;
                switch (cc)
                {
                    case CacheCategory.STD:
                        if (name.StartsWith(CacheCategory.OPC.ToString()) || name.StartsWith(CacheCategory.OSC.ToString()))
                            continue;
                        break;
                    case CacheCategory.OPC:
                    case CacheCategory.OSC:
                        if (!name.StartsWith(key)) continue;
                        break;
                    default:
                        break;
                }
                HttpRuntime.Cache.Remove((string)enumerator.Key);
            }

        }




        public static void DestroySingleSecondCache(string key)
        {
            System.Collections.IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var name = (string)enumerator.Key;
                if (name.IndexOf(key) >= 0)
                    HttpRuntime.Cache.Remove((string)enumerator.Key);
            }

        }




        public static IEnumerable<SecondCacheItemProperty> GetSecondCache(CacheCategory cc)
        {

            string CacheKey = string.Empty;
            IList<SecondCacheItemProperty> cache = new List<SecondCacheItemProperty>();

            System.Collections.IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                CacheKey = (string)enumerator.Key;

                switch (cc)
                {
                    case CacheCategory.STD:
                        if (CacheKey.StartsWith(CacheCategory.OPC.ToString()) || CacheKey.StartsWith(CacheCategory.OSC.ToString()))
                        {
                            continue;
                        }
                        cache.Add(GetCacheObjectProperty(CacheKey));
                        break;
                    default:
                        if (CacheKey.StartsWith(cc.ToString()))
                            cache.Add(GetCacheObjectProperty(CacheKey));

                        break;
                }
            }

            return cache;
        }






        #endregion












    }



}
