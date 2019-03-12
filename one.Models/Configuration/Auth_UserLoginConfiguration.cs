using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using one.Data.Entities;


namespace one.Data.Configuration
{
    public class Auth_UserLoginConfiguration : EntityTypeConfiguration<Auth_UserLogin>
    {

        public Auth_UserLoginConfiguration() {

            Map(c =>
            {

                c.ToTable("Auth_UserLogin");
                c.Properties(p => new
                {
                    p.UserId,
                    p.LoginProvider,
                    p.ProviderKey,
                    p.Remark,
                });
            });

            HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

        }

    }
}
