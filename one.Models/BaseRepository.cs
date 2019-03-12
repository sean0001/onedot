using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace one.Data
{
    public class BaseRepository
    {
        //protected static FrameContext Sdb {
        //    get {
        //        return FrameContext.Sdb;
        //    }
        //}


        //protected static FrameContext Db {

        //    get {
        //        return new FrameContext();
        //    }

        //}

        [ThreadStatic]
        private static ApplicationDbContext _Sdb;

        public static ApplicationDbContext Sdb
        {
            get
            {
                if (_Sdb == null)
                    _Sdb = new ApplicationDbContext();

                return _Sdb;
            }
        }


        private ApplicationDbContext _Db;
        public ApplicationDbContext Db
        {
            get
            {
                if (_Db == null)
                    _Db = new ApplicationDbContext();
                return _Db;
            }
        }



    }
}
