using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using one.Data.Models;
using one.Data.Infrastructrue;

namespace one.Data.Repository
{
    public class RoleRepository : RepositoryBase<Auth_Roles>, IRoleRepository
    {

        public RoleRepository()
            : base()
        {

        }


        public RoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }




    }




    public interface IRoleRepository : IRepository<Auth_Roles>
    {


    }
}
