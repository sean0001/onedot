using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using one.Data;
using one.Data.Models;
using one.Data.Repository;

namespace one.Service
{


    public class RoleService : RoleRepository
    {




      public bool DelUserRole(string UserId , int RoleId) {
          bool result = false;

    

          return result;
      }




      public bool AddRole(string RoleName) {
          bool result = false;
          var role = new Auth_Roles(){ Name =""};
          DataContext.Auth_Roles.Add(role);
          DataContext.Commit();
          return result;
      }



        public IEnumerable<OSelectListItem> GetRolesSelectList()
        {
            return DataContext.Auth_Roles
                .Select(s => new OSelectListItem()
                {
                    Value = s.RoleId,
                    Text = s.Name,
                    Describe = s.Describe
                }).ToList();
        }


        public IEnumerable<ViewRole> GetRoles()
        {

            return DataContext.Auth_Roles
                .Select(s => new ViewRole()
                {
                    RoleId = s.RoleId,
                    RoleName = s.Name,
                    Describe = s.Describe
                }).ToList();
        }




        public void UpdateRole(ref ViewRole role) {

            Auth_Roles entity = new Auth_Roles() {
                RoleId = role.RoleId ?? one.Core.Utilities.ShortGuid(),
                Name = role.RoleName,
                Describe = role.Describe
            };

            DataContext.Auth_Roles.Attach(entity);
            DataContext.Entry(entity).State = getEntitystate(role.RoleId);
            DataContext.Commit();

        }


        public void DestoryRole(ref ViewRole role) {

            Auth_Roles entity = new Auth_Roles()
            {
                RoleId = role.RoleId
            };

            DataContext.Auth_Roles.Attach(entity);
            DataContext.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            DataContext.Commit();

        }





    }
}
