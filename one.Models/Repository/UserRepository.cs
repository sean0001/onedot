using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using one.Data.Models;
using one.Data.Infrastructrue;



namespace one.Data.Repository
{
    public class UserRepository : RepositoryBase<Auth_Users>, IUserRepository
    {

         public UserRepository()
            : base()
        {

        }


         public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {


        }
    }


     public interface IUserRepository : IRepository<Auth_Users>
    {


    }

}
