using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using one.Data.Entities;
using one.Data.Configuration;
namespace one.Data
{
 


    public class ApplicationDbContext : 
        IdentityDbContext<Auth_Users, Auth_Roles, string, Auth_UserLogin, Auth_UserRole, Auth_UserClaim>
       {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new DBInitializer());
        }




        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Configurations.Add(new Auth_UserLoginConfiguration());

            modelBuilder.Configurations.Add(new Auth_RolesConfiguration());

            modelBuilder.Configurations.Add(new Auth_UsersConfiguration());

            modelBuilder.Configurations.Add(new Auth_UserInforConfiguration());

            modelBuilder.Configurations.Add(new Auth_UserRoleConfiguration());

            modelBuilder.Configurations.Add(new Auth_UserClaimConfiguration());

            modelBuilder.Configurations.Add(new Auth_FuncTreeConfiguration());

            modelBuilder.Configurations.Add(new Pub_MetadataConfiguration());
            
            modelBuilder.Configurations.Add(new Auth_RoleFuncConfiguration());


        }



        
        public virtual DbSet<Auth_FuncTree> Auth_FuncTree { get; set; }
        public virtual DbSet<Auth_RoleFunc> Auth_RoleFunc { get; set; }
        public virtual DbSet<Pub_MetaIndex> Pub_MetaIndex { get; set; }
        public virtual DbSet<Pub_Metadata> Pub_Metadata { get; set; }

        

    }
}