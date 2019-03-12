using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using one.OneDot.Areas.Stocks.Models;

namespace one.OneDot.Areas.Stocks.Repository
{
    public class BaseRepository
    {
        /// <summary>
        /// 使用普通方式连接
        /// </summary>
        protected static StocksDb sdb
        {
            get
            {
                if (_sdb == null)
                    _sdb = new StocksDb();
                return _sdb;
            }
        }
        private static StocksDb _sdb { get; set; }



        /// <summary>
        /// 使用 entityframework 方式连接
        /// </summary>
        protected static StockEntities sedb
        {
            get
            {
                if (_sedb == null)
                    _sedb = new StockEntities();
                return _sedb;
            }
        }
        private static StockEntities _sedb { get; set; }



    }
}