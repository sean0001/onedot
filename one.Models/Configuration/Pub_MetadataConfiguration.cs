using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using one.Data.Entities;



namespace one.Data.Configuration
{
   public class Pub_MetadataConfiguration :EntityTypeConfiguration<Pub_Metadata>
    {

       public Pub_MetadataConfiguration() { 
       HasKey(r => new { r.Id });
       HasMany(s => s.Modules).WithOptional().HasForeignKey(a => a.ModuleId).WillCascadeOnDelete(false);
       HasMany(s => s.FuncTypes).WithOptional().HasForeignKey(a => a.FuncTypeId).WillCascadeOnDelete(false);
       }

    }
}
