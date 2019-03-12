using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;




namespace one.Infras.DataCache
{
    public static class CCache
    {


        /// <summary>
        /// 泛型委托返回IEnumerable数据集
        /// </summary>
        /// <param name="func"> </param>
        /// <param name="Id"> 有一个字符参数</param>
        /// <param name="DependTable">查询所依赖的tabe字符串 </param>
        /// <returns></returns>
        public static IEnumerable<T> getEntities<T>(Func<string, IEnumerable<T>> func, string name, string DependTable = CustomCache.None, bool ReadCache = true)
        {

            

            string cachekey = string.Format("{2}-{0}-{1}", func.Method.Name, name, CustomCache.CachePrefixWord);

            // var cache = HttpContext.Current.Cache.Get(cachekey);
            var cache = HttpRuntime.Cache.Get(cachekey);

            if (cache != null)
            {
                if (ReadCache) return (IEnumerable<T>)cache; /// readcache = true 返回缓存数据
                HttpRuntime.Cache.Remove(cachekey);  /// readcache = false 清除缓存，读取数据库
            }


            var m = func(name);

            if (ReadCache) insertCache(cachekey, m, DependTable);

            return m;
        }




        /// <summary>
        /// 泛型委托返回IEnumerable数据集
        /// </summary>
        /// <param name="func"> </param>
        /// <param name="Id"> 无参数参数</param>
        /// <param name="DependTable">查询所依赖的tabe字符串 </param>
        /// <returns></returns>
        public static List<T> getEntities<T>(Func<List<T>> func, string DependTable = CustomCache.None, bool ReadCache = true)
        {

            string cachekey = string.Format("{1}-{0}", func.Method.Name, CustomCache.CachePrefixWord);
            // var cache = HttpContext.Current.Cache.Get(cachekey);
            var cache = HttpRuntime.Cache.Get(cachekey);
            if (cache != null)
            {
                if (ReadCache) return (List<T>)cache; /// readcache = true 返回缓存数据
                HttpRuntime.Cache.Remove(cachekey);  /// readcache = false 清除缓存，读取数据库
            }

            var m = func();
            if (ReadCache) insertCache(cachekey, m, DependTable);
            return m;
        }






        /// <summary>
        /// 泛型委托返回IEnumerable数据集
        /// </summary>
        /// <param name="func"> </param>
        /// <param name="Id"> 无参数参数</param>
        /// <param name="DependTable">查询所依赖的tabe字符串 </param>
        /// <returns></returns>
        public static IEnumerable<T> getEntitiesNoParm<T>(Func<IEnumerable<T>> func, string KeyWord, string DependTable = CustomCache.None, bool ReadCache = true)
        {

            string cachekey = KeyWord;
            // var cache = HttpContext.Current.Cache.Get(cachekey);
            var cache = HttpRuntime.Cache.Get(cachekey);
            if (cache != null)
            {
                if (ReadCache) return (IEnumerable<T>)cache; /// readcache = true 返回缓存数据
                HttpRuntime.Cache.Remove(cachekey);  /// readcache = false 清除缓存，读取数据库
            }

            var m = func();
            if (ReadCache) insertCache(cachekey, m, DependTable);
            return m;
        }








        /// <summary>
        /// 泛型委托返回IEnumerable数据集
        /// </summary>
        /// <param name="func"> </param>
        /// <param name="Id"> 无参数参数</param>
        /// <param name="DependTable">查询所依赖的tabe字符串 </param>
        /// <returns></returns>
        public static IEnumerable<T> getEntities<T>(Func<IEnumerable<T>> func, string DependTable = CustomCache.None, bool ReadCache = true)
        {


            string cachekey = string.Format("{1}-{0}", func.Method.Name, CustomCache.CachePrefixWord);
            // var cache = HttpContext.Current.Cache.Get(cachekey);
            var cache = HttpRuntime.Cache.Get(cachekey);

            if (cache != null)
            {
                if (ReadCache) return (IEnumerable<T>)cache; /// readcache = true 返回缓存数据
                HttpRuntime.Cache.Remove(cachekey);  /// readcache = false 清除缓存，读取数据库

                //  System.Runtime.Caching.MemoryCache.Default["names"];
            }

            var m = func();
            if (ReadCache) insertCache(cachekey, m, DependTable);
            return m;
        }




        /// <summary>
        /// 泛型委托返回IEnumerable数据集
        /// </summary>
        /// <param name="func"> </param>
        /// <param name="Id"> 有一个id参数</param>
        /// <param name="DependTable">查询所依赖的tabe字符串 </param>
        /// <returns></returns>
        public static IEnumerable<T> getEntities<T>(Func<int, IEnumerable<T>> func, int Id, string DependTable = CustomCache.None, bool ReadCache = true)
        {


            string cachekey = string.Format("{2}-{0}-{1}", func.Method.Name, Id, CustomCache.CachePrefixWord);

            // var cache = HttpContext.Current.Cache.Get(cachekey);
            var cache = HttpRuntime.Cache.Get(cachekey);

            if (cache != null)
            {
                if (ReadCache) return (IEnumerable<T>)cache; /// readcache = true 返回缓存数据
                HttpRuntime.Cache.Remove(cachekey);  /// readcache = false 清除缓存，读取数据库
            }


            var m = func(Id);

            if (ReadCache) insertCache(cachekey, m, DependTable);

            return m;
        }



        /// <summary>
        /// 根据id返回单个实体 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="Id"></param>
        /// <param name="DependTable">数据所依赖的数据库表</param>
        /// <returns></returns>
        public static T getEntity<T>(Func<int, T> func, int Id, string DependTable = CustomCache.None, bool ReadCache = true)
        {

            string cachekey = string.Format("{2}-{0}-{1}", func.Method.Name, Id, CustomCache.CachePrefixWord);
            var cache = HttpContext.Current.Cache.Get(cachekey);

            if (cache != null)
            {

                if (ReadCache) return (T)cache; /// readcache = true 返回缓存数据

                HttpRuntime.Cache.Remove(cachekey);  /// readcache = false 清除缓存，读取数据库
            }

            var m = func(Id);

            if (ReadCache) insertCache(cachekey, m, DependTable);

            return m;
        }



        ///// <summary>
        ///// 根据字符串id返回单个实体 
        ///// </summary>
        ///// <param name="func"></param>
        ///// <param name="Id"></param>
        ///// <param name="DependTable">数据所依赖的数据库表</param>
        ///// <returns></returns>
        //public static T getEntity<T>(Func<string, T> func, string strId, string DependTable = CustomCache.None, bool ReadCache = true)
        //{

        //    string cachekey = string.Format("{2}-{0}-{1}", func.Method.Name, strId, CustomCache.CachePrefixWord);
        //    var cache = HttpContext.Current.Cache.Get(cachekey);

        //    if (cache != null)
        //    {
        //        if (ReadCache) return (T)cache; /// readcache = true 返回缓存数据
        //        HttpRuntime.Cache.Remove(cachekey);  /// readcache = false 清除缓存，读取数据库
        //    }

        //    var m = func(strId);

        //    if (ReadCache) insertCache(cachekey, m, DependTable);

        //    return m;
        //}





        /// <summary>
        /// 返回单个实体
        /// </summary>
        /// <param name="func"></param>
        /// <param name="Id"></param>
        /// <param name="DependTable">数据所依赖的数据库表</param>
        /// <returns></returns>
        public static T getEntity<T>(Func<string, T> func, 
            string CacheKeyWord, 
            string Id, 
            string DependTable = CustomCache.None, 
            bool ReadCache = true)
           {

            string cachekey = string.Format(CacheKeyWord, Id);

            var cache = HttpRuntime.Cache.Get(cachekey);

            if (cache != null)
            {
                if (ReadCache) return (T)cache; /// readcache = true 返回缓存数据
                HttpRuntime.Cache.Remove(cachekey);  /// readcache = false 清除缓存，读取数据库
            }

            var m = func(Id);
            if (ReadCache)
                insertCache(cachekey, m, DependTable);

            return m;
        }












        /// <summary>
        /// 删除缓存数据， 在更新操作时执行 缓存删除操作
        /// </summary>
        /// <param name="id"></param>
        public static void removeEntity(string CacheKeyWord, string id)
        {
            string cachekey = string.Format(CacheKeyWord, id);
            HttpRuntime.Cache.Remove(cachekey);
        }




        public static void removeEntity(string CacheKeyWord)
        {
            string cachekey = CacheKeyWord;
            HttpRuntime.Cache.Remove(cachekey);

        }


        /// <summary>
        /// 根据通用的sql依赖缓存字符,生成合成的缓存对象
        /// </summary>
        /// <param name="csStr"></param>
        /// <returns></returns>
        private static AggregateCacheDependency AggregateCacheDepend(string csStr)
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



        private static void insertCache(string CacheKey, object m, string CacheDepString)
        {

            var sqldep = AggregateCacheDepend(CacheDepString);
            lock (HttpRuntime.Cache)
            {
                HttpRuntime.Cache.Insert(CacheKey, m, sqldep, Cache.NoAbsoluteExpiration, new TimeSpan(0, 60, 0));
                
            }

        }


        #region  缓存维护  好像不能使用

        //public static long GetObjSizeInMemo(object o)
        //{
        //    long lSize = 0;
        //    try
        //    {
        //        System.IO.MemoryStream stream = new System.IO.MemoryStream();
        //        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        //        objFormatter.Serialize(stream, o);
        //        lSize = stream.Length;
        //        return lSize;
        //    }
        //    catch (Exception excp)
        //    {
        //        return -1;
        //    }
        //}





        //public static CacheObjProperty GetCacheObjectProperty(string cacheKey)
        //{
        //    object cacheEntry = HttpRuntime.Cache.GetType()
        //        .GetMethod("Get", BindingFlags.Instance | BindingFlags.NonPublic)
        //        .Invoke(HttpRuntime.Cache, new object[] { cacheKey, 1 });


        //    PropertyInfo utcExpiresProperty = cacheEntry.GetType()
        //        .GetProperty("UtcExpires", BindingFlags.NonPublic | BindingFlags.Instance);

        //    PropertyInfo UtcCreatedProperty = cacheEntry.GetType()
        //           .GetProperty("UtcCreated", BindingFlags.NonPublic | BindingFlags.Instance);

        //    PropertyInfo UsageBucketProperty = cacheEntry.GetType()
        //           .GetProperty("UsageBucket", BindingFlags.NonPublic | BindingFlags.Instance);


        //    PropertyInfo UtcLastUsageUpdateProperty = cacheEntry.GetType()
        //           .GetProperty("UtcLastUsageUpdate", BindingFlags.NonPublic | BindingFlags.Instance);



        //    return new CacheObjProperty
        //    {
        //        id = new Guid(),
        //        Name = cacheKey,
        //        UtcLastUsageUpdate = ((DateTime)UtcLastUsageUpdateProperty.GetValue(cacheEntry, null)).ToLocalTime(),
        //        UsageBucket = (byte)UsageBucketProperty.GetValue(cacheEntry, null),
        //        UtcCreated = ((DateTime)UtcCreatedProperty.GetValue(cacheEntry, null)).ToLocalTime(),
        //        utcExpires = (DateTime)utcExpiresProperty.GetValue(cacheEntry, null)
        //    };

        //}


        //public static void DestroyUserCache()
        //{

        //    System.Collections.IDictionaryEnumerator enumerator = System.Web.HttpRuntime.Cache.GetEnumerator();

        //    while (enumerator.MoveNext())
        //    {
        //        var name = (string)enumerator.Key;
        //        if (name.IndexOf(CustomCache.CachePrefixWord) > 0)

        //            HttpRuntime.Cache.Remove((string)enumerator.Key);

        //    }


        //}

        #endregion



    }



    public class CustomCache
    {


        /// <summary>
        /// 数据库依赖缓存中的 缓存名称 前缀字符串
        /// </summary>
        public const string CachePrefixWord = "[CCIC]";

        public static readonly string UserInfo = "UserInfo-{0}";

        public static readonly string articleList = "articleList";

        public const string None = "";

        public static readonly string webpages_UsersInRoles = "BYCache:webpages_UsersInRoles";
        
        public static readonly string permissionMemoes = "BYCache:permissionMemoes";

        public static readonly string roleAct = "BYCache:roleAct";

        /// <summary>
        /// 最后不能有冒号
        /// </summary>
        /// 

        public const string FlowProcessDefine = "BYCache:Flow_FlowProcessDefine";

        public const string getCorpSelectList = "BYCache:corpSet";

        public const string ImportRoomState = "BYCache:RS_roomStateMemo;BYCache:RS_uploadRoomState";

        public const string OrderMemoModify = "BYCache:bill_main;BYCache:checkInRoomClass;BYCache:bill_roomtype;BYCache:bill_roomMemos;BYCache:bill_MainExtend;BYCache:bill_GoBackOrder;BYCache:bill_Attachment;BYCache:userWorkGroup;BYCache:bill_AdditionalOrder;BYCache:bill_traveller;BYCache:bill_roomGroup";

        public const string AttaOrder = "BYCache:bill_main;BYCache:bill_AdditionalOrder";

        public const string IslandSet_1 = "BYCache:IslandSet;BYCache:roomTypeSet;BYCache:islandRoomTypeSet";

        public static readonly string Transport = "BYCache:trafficSet";

        public static readonly string roomMemos = "BYCache:bill_roomMemos;BYCache:bill_traveller";

        public static readonly string RoomView = "BYCache:bill_roomMemos";

        public const string HotelRoomName = "BYCache:hotel_RoomName;BYCache:roomTypeSet";

        public const string RoomEvents = "BYCache:hotel_events;BYCache:hotel_roomEvents";

        public const string attaOrderLog = "BYCache:bill_AttaOrder_log";

        public const string HOTEL_ISLAND_ROOM = "BYCache:IslandSet;BYCache:corpSet;BYCache:islandRoomTypeSet;BYCache:roomTypeSet";

        public const string BindOrdersBills = "BYCache:matchBill_attachInfo;BYCache:matchBill_main;BYCache:matchBill_order;BYCache:matchBill_remark";


        public static readonly string HotelRoomMemo = "BYCache:bill_roomtype;BYCache:trafficSet;BYCache:hotel_checkInMemo;BYCache:bill_traveller;BYCache:bill_roomMemos";

        /// <summary>
        /// kendo 分页功能更新的参数 VaryByParam
        /// </summary>
        public const string GridVaryByParam = "sort;page;pageSize;group;filter";
    }



}
