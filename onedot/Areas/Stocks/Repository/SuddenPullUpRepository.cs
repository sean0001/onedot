using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;



namespace one.OneDot.Areas.Stocks.Repository
{


    public class M_hoverPickup
    {
        public string name { get; set; }
        public string industry { get; set; }
        public string area { get; set; }
        public string stockcode { get; set; }
        public decimal? d1 { get; set; }
        public decimal? v1 { get; set; }
        public decimal? d2 { get; set; }
        public decimal? v2 { get; set; }
        public decimal? d3 { get; set; }
        public decimal? v3 { get; set; }
        public decimal? d4 { get; set; }
        public decimal? v4 { get; set; }
        public decimal? d5 { get; set; }
        public decimal? v5 { get; set; }
        public decimal? d6 { get; set; }
        public decimal? v6 { get; set; }
        public decimal? d7 { get; set; }
        public decimal? v7 { get; set; }
        public decimal? d8 { get; set; }
        public decimal? v8 { get; set; }
        public decimal? d9 { get; set; }
        public decimal? v9 { get; set; }
        public decimal? d10 { get; set; }
        public decimal? v10 { get; set; }
        public decimal? d11 { get; set; }
        public decimal? v11 { get; set; }
        public decimal? d12 { get; set; }
        public decimal? v12 { get; set; }
        public decimal? d13 { get; set; }
        public decimal? v13 { get; set; }
        public decimal? d14 { get; set; }
        public decimal? v14 { get; set; }
        public decimal? d15 { get; set; }
        public decimal? v15 { get; set; }
        public decimal? d16 { get; set; }
        public decimal? v16 { get; set; }
        public decimal? d17 { get; set; }
        public decimal? v17 { get; set; }
        public decimal? d18 { get; set; }
        public decimal? v18 { get; set; }
        public decimal? d19 { get; set; }
        public decimal? v19 { get; set; }
        public decimal? d20 { get; set; }
        public decimal? v20 { get; set; }
    }


    public class HoverPickupRepository :BaseRepository
    {

        public static List<one.OneDot.Areas.Stocks.Models.st_dayDataHistory> gettest() {

            return sedb.st_dayDataHistory.Take(10).ToList();

        }



        public static List<M_hoverPickup> getHoverPickup(DateTime endDate, decimal fluctuationRange, int days,decimal volumeRate,int UpOrDown) {

            return sedb.Database.SqlQuery<M_hoverPickup>("Pro_hoverPickup @endDate,@fluctuationRange,@days,@volumeRate,@UpOrDown",
                new SqlParameter("endDate", endDate),
                new SqlParameter("fluctuationRange", fluctuationRange),
                new SqlParameter("days", days),
                new SqlParameter("VolumeRate", volumeRate),
                new SqlParameter("UpOrDown", UpOrDown)
                ).ToList();

        

        }



    }
}