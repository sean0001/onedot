using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using one.Data.Models;
using one.Data.Repository;


namespace one.Service
{


    public class ViewRole {
        public string RoleId { get; set; }
        [Display(Name = "RoleName", ResourceType = typeof(one.Res.Lang))]
        public string RoleName { get; set; }

        [Display(Name = "RoleDescribe", ResourceType = typeof(one.Res.Lang))]
        public string Describe { get; set; }

    }



    public class ViewRolesOfUser {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Describe { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }

        [UIHint("_MultiSelect")]
        public IEnumerable<OSelectListItem> Roles { get; set; }

    }



   







    public class UserRoleService : RoleRepository
    {





        public void updateRoleForUser(string UserId, String[] Roles) {


            string sqlClear = "delete from auth_userRole where userId = {0}";
            string sqlInsert = "insert into auth_userRole(userId,roleId) values({0},{1})";

            DataContext.Database.ExecuteSqlCommand(sqlClear, UserId);
            if (Roles != null)
            {
                for (int i = 0; i < Roles.Length; i++)
                {
                    DataContext.Database.ExecuteSqlCommand(sqlInsert, UserId,Roles[i]);
                }
            }

            DataContext.Commit();
        }






        /// <summary>
        /// add roles for user
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Roles"></param>
        public void AddRoleForUser(string UserId,String[] Roles) {


            if (Roles == null) { 
            
            }

            for (int i = 0; i <  Roles.Length; i++)
            {
               var m = DataContext.Auth_Users.Find(UserId);
               var b = DataContext.Auth_Roles.Find(Roles[i]);
               m.Auth_Roles.Add(b);
            }

            DataContext.Commit();

        }


        /// <summary>
        /// delete matching roles of user 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Roles"></param>
        public void DeleteRoleForUser(string UserId, String[] Roles)
        {

            for (int i = 0; i < Roles.Length; i++)
            {
                var m = DataContext.Auth_Users.Find(UserId);
                var b = DataContext.Auth_Roles.Find(Roles[i]);
                m.Auth_Roles.Remove(b);
            }

            DataContext.Commit();

        }


        public string[] GetRolesNameOfUser(string UserId) {

            return DataContext.Auth_Users.Where(a => a.UserId == UserId)
              .SelectMany(s => s.Auth_Roles.Select(ss => ss.Name)).ToArray();       
        }




        public IEnumerable<ViewRolesOfUser> GetUserAndRoles()
        {


            return DataContext.Auth_Users.Select(s => new ViewRolesOfUser()
               {
                   UserId = s.UserId,
                   Email = s.Email,
                   UserName = s.UserName,
                   CreateTime = s.CreateTime,
                   PhoneNumber = s.PhoneNumber,
                   Remark = s.Auth_UserInfor.Remark,
                   Describe = s.Auth_UserInfor.Describe,
                   Roles = s.Auth_Roles.Select(a => new OSelectListItem() { 
                       Value = a.RoleId,
                       Text = a.Name,
                       Describe = a.Describe
                   })
               }).ToList();

        }



        public IEnumerable<OSelectListItem> GetRolesAll()
        {

            return DataContext.Auth_Roles.Select(a => new OSelectListItem
            {
                Value = a.RoleId,
                Text = a.Name,
                Describe = a.Describe
            }).ToList();
        }




        //public IEnumerable<>



        //public string[] GetRolesNameOfUser(string UserId)
        //{

        //    return DataContext.Auth_Users.Where(a => a.UserId == UserId)
        //      .SelectMany(s => s.Auth_Roles.Select(ss => ss.RoleId)).ToArray();
        //}


    }
}
