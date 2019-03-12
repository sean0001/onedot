using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using one.Data.Entities;

namespace one.Data.Configuration
{
   public class Auth_RoleFuncConfiguration : EntityTypeConfiguration<Auth_RoleFunc>
    {

       public Auth_RoleFuncConfiguration() {

           Map(c =>
           {
               c.ToTable("Auth_RoleFunc");
               c.Properties(a => new
               {
                   a.RoleId,
                   a.FuncId
               });
           }).HasKey(c => new { c.RoleId, c.FuncId });

       
       }
    }
}
