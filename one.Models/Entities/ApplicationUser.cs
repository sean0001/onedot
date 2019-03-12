using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using one.Core;


namespace one.Data.Entities
{
    public class Auth_Users : IdentityUser<string, Auth_UserLogin, Auth_UserRole, Auth_UserClaim>
    {


        public Auth_Users() {
            this.Id = Utilities.ShortGuid();
            this.CreateTime = DateTime.Now;
        }

        public DateTime CreateTime { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Auth_Users> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // 在此处添加自定义用户声明
            return userIdentity;
        }

        public virtual Auth_UserInfor Auth_UserInfor { get; set; }


    }

}
