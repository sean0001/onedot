using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using one.Data.Models;
using System.Transactions;
using one.Data.Infrastructrue;


namespace one.Data
{

    public class DBContext : Disposable, IDatabaseFactory
    {
        private OneDotEntities dataContext;
        public OneDotEntities Get()
        {
            return dataContext ?? (dataContext = new OneDotEntities());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }



    //public class Conn
    //{
    //    private static OneDotEntities _StaticDb { get; set; }
    //    public static OneDotEntities StaticDb
    //    {
    //        get
    //        {
    //            if (_StaticDb == null)
    //                _StaticDb = new OneDotEntities();
    //            return _StaticDb;

    //        }
    //    }


    //    public static void Commit(TransactionScope ts = null){

    //        try
    //        {
    //            _StaticDb.SaveChanges();
    //            if (ts != null)
    //                ts.Complete();
    //        }
    //        catch (Exception )
    //        {
                
    //            throw;
    //        }
    //    }
    //}
}



