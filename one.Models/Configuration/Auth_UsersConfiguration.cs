using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using one.Data.Entities;

namespace one.Data.Configuration
{
  
    public class Auth_UsersConfiguration : EntityTypeConfiguration<Auth_Users>
    {
        public Auth_UsersConfiguration()
        {

            Property(a => a.Email).HasMaxLength(50);
            Property(a => a.PhoneNumber).HasMaxLength(50);
            Property(a => a.UserName).HasMaxLength(50);
            Property(a => a.Email).HasMaxLength(50);
            Map(c =>
               {
                   c.ToTable("Auth_Users");
                   c.Property(p => p.Id).HasColumnName("UserId");
                   c.Properties(p => new
                   {
                       
                       p.UserName,
                       p.Email,
                       p.EmailConfirmed,
                       p.PasswordHash,
                       p.PhoneNumber,
                       p.PhoneNumberConfirmed,
                       p.TwoFactorEnabled,
                       p.SecurityStamp,
                       p.LockoutEnabled,
                       p.LockoutEndDateUtc,
                       p.AccessFailedCount,
                       p.CreateTime,
                      
                   });
               });

            HasKey(a => a.Id);
            HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);
            HasOptional(c => c.Auth_UserInfor).WithRequired(c => c.AuthUser);





        }
    }


}
