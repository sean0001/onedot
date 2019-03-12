using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using one.OneDot.Areas.Stocks.Models;
using System.Collections;

namespace one.OneDot.Areas.Stocks.Repository
{


    public class _singleDayLine {

        public DateTime Date { get; set; }

        public decimal Open { get; set; }
        public decimal High { get; set;
        }
        public decimal Low { get; set; }
        public decimal Close { get; set; }

        public decimal Volume { get; set; }

        public string Symbol { get; set; }

    }




    public class SingleDayLine {
        public decimal? price { get; set; }
        public DateTime dealdate { get; set; }

    }



    public class BaseStockRepository : BaseRepository
    {



   

        public static IEnumerable<_singleDayLine> GetSingleDayLine(string stockcode) {

            //return sedb.dayDataHistory.Where(a => a.stockCOde == stockcode)
            //    .Select(s => new SingleDayLine()
            //    {
            //        dealdate = s.pDate,
            //        price = s.close
            //    });


            return sedb.st_dayDataHistory.Where(a => a.stockCOde == stockcode)
                
                
                .Select(s => new _singleDayLine()
                {
                    Date = s.pDate,
                    Close = s.close ?? 0,
                    High = s.high ?? 0,
                    Open = s.open ?? 0,
                    Low = s.low ?? 0,
                    Volume = s.volume ?? 0,
                    Symbol = "bbbb"
                }).OrderByDescending(a=>a.Date);




        }



    }
}