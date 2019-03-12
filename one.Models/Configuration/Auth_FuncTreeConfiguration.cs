using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using one.Data.Entities;

namespace one.Data.Configuration
{
    public class Auth_FuncTreeConfiguration : EntityTypeConfiguration<Auth_FuncTree>
    {

        public Auth_FuncTreeConfiguration() {

            HasOptional(a => a.Parent)
                .WithMany(s => s.Children)
                .WillCascadeOnDelete(false);

        }


    }
}
