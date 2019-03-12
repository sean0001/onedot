using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using one.Data.Entities;


namespace one.Data.Configuration
{
    public class Auth_UserInforConfiguration :EntityTypeConfiguration<Auth_UserInfor>
    {

         

        public Auth_UserInforConfiguration() {

            Property(a => a.Area).HasMaxLength(50);
            Property(a => a.Department).HasMaxLength(50);
            Property(a => a.Describe).HasMaxLength(100);
            Property(a => a.Mobile).HasMaxLength(50);
            Property(a => a.Name).HasMaxLength(50);
            Property(a => a.QQ).HasMaxLength(50);
            Property(a => a.LastUpdateTime)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
        }

    }
}
