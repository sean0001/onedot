using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using one.Data.Models;
using one.Data.Infrastructrue;



namespace one.Data.Repository
{

    public class ApplicationConfigRepository : RepositoryBase<Pub_Metadata>, IApplicationConfigRepository
    {

        public ApplicationConfigRepository()
            : base()
        {

        }


        public ApplicationConfigRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }




    }




    public interface IApplicationConfigRepository : IRepository<Pub_Metadata>
    {


    }

}
