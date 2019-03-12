using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace one.OneDot.Areas.Stocks.Repository
{
    public class StocksDb : DbContext
    {
        public StocksDb()
            : base("name = Stocks")
        {
        }


    }

}