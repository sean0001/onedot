using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using one.Data.Entities;

namespace one.Data.Configuration
{
    public class Auth_UserRoleConfiguration : EntityTypeConfiguration<Auth_UserRole>
    {
        public Auth_UserRoleConfiguration()
        {

            Map(c =>
            {
                c.ToTable("Auth_UserRole");
                c.Properties(p => new
                {
                    p.UserId,
                    p.RoleId
                });
            });
            HasKey(c => new { c.UserId, c.RoleId });

        }

    }
}
