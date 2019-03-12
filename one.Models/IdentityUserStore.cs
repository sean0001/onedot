using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using one.Data.Entities;



namespace one.Data
{
    public class IdentityUserStore :
     UserStore<Auth_Users, Auth_Roles, string, Auth_UserLogin, Auth_UserRole, Auth_UserClaim>,
     IUserStore<Auth_Users>,
     IDisposable
    {
        public IdentityUserStore(ApplicationDbContext context)
            : base(context)
        {

        }
    }


}
