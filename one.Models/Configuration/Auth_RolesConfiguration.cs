using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using one.Data.Entities;

namespace one.Data.Configuration
{
    public class Auth_RolesConfiguration : EntityTypeConfiguration<Auth_Roles>
    {

        public Auth_RolesConfiguration()
        {

            Map(c =>
             {
                 c.ToTable("Auth_Roles");
                 c.Property(p => p.Id).HasColumnName("RoleId");
                 c.Properties(p => new
                 {
                     p.Name,
                     p.Describe,
                     p.NameEn,
                     p.DescribeEn
                 });
             });

            HasKey(p => p.Id);
            Property(c => c.Name).HasMaxLength(100);
            Property(c => c.Describe).HasMaxLength(1000);
            Property(c => c.NameEn).HasMaxLength(100);
            Property(c => c.DescribeEn).HasMaxLength(100);
            HasMany(c => c.Users).WithRequired().HasForeignKey(c => c.RoleId);
            

        }


    }
}
