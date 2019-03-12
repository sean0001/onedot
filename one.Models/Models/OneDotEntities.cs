using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Transactions;
using one.Core;
namespace one.Data.Models
{
    public partial class OneDotEntities : DbContext
    {

        

        public OneDotEntities()
            : base("name=OneDotEntities")
        {

        }
        

          public virtual void Commit(TransactionScope ts = null)
        {
                base.SaveChanges();
                if (ts != null)
                    ts.Complete();
        }


    }
}
