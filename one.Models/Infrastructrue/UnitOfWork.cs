using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace one.Data.Infrastructrue
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private one.Data.Models.OneDotEntities dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected one.Data.Models.OneDotEntities DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            DataContext.Commit();
        }
    }
}
