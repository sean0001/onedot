using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using one.Data.Entities;

namespace one.Data.Configuration
{
  public class Auth_UserClaimConfiguration : EntityTypeConfiguration<Auth_UserClaim>
    {

      public Auth_UserClaimConfiguration() {

          Map(c =>
          {
              c.ToTable("Auth_UserClaim");
              c.Property(p => p.Id).HasColumnName("UserClaimId");
              c.Properties(p => new
              {
                  p.UserId,
                  p.ClaimValue,
                  p.ClaimType
              });
          }).HasKey(c => c.Id);
      
      }
    }
}
