using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using one.Data;
using one.Data.Models;
using one.Data.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace one.Service
{



    public class ViewUser {
        public string UserId { get; set; }


        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "Btn_LockOrUnlock", ErrorMessageResourceType = typeof(one.Res.Lang))]
        [Required(ErrorMessageResourceName = "Btn_LockOrUnlock", ErrorMessageResourceType = typeof(one.Res.Lang))]
        //[RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessageResourceName = "Btn_LockOrUnlock", ErrorMessageResourceType = typeof(one.Res.Lang))]
        [Display(Name = "UserName", ResourceType = typeof(one.Res.Lang))]
        public string UserName { get; set; }



        [Display(Name = "NickName", ResourceType = typeof(one.Res.Lang))]
        public string Name { get; set; }


        [Display(Name = "CreateTime", ResourceType = typeof(one.Res.Lang))]
        public System.DateTime CreateTime { get; set; }



        [Display(Name = "LastUpdateTime", ResourceType = typeof(one.Res.Lang))]
        public System.DateTime? LastUpdateTime { get; set; }



        [Display(Name = "Email", ResourceType = typeof(one.Res.Lang))]
        public string Email { get; set; }


        [Display(Name = "EmailConfirmed", ResourceType = typeof(one.Res.Lang))]
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }


        [Display(Name = "PhoneNumber", ResourceType = typeof(one.Res.Lang))]
        public string PhoneNumber { get; set; }


        [Display(Name = "MobileNumber", ResourceType = typeof(one.Res.Lang))]
        public string Mobile { get; set; }




        [Display(Name = "PhoneNumberConfirmed", ResourceType = typeof(one.Res.Lang))]
        public bool PhoneNumberConfirmed { get; set; }


        [Display(Name = "LockoutEnabled", ResourceType = typeof(one.Res.Lang))]
        public bool LockoutEnabled { get; set; }


        [Display(Name = "LockoutEndDateUtc", ResourceType = typeof(one.Res.Lang))]
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }


        [Display(Name = "AccessFailedCount", ResourceType = typeof(one.Res.Lang))]
        public int AccessFailedCount { get; set; }
        public string Department { get; set; }
        public string Area { get; set; }


        [Display(Name = "Describe", ResourceType = typeof(one.Res.Lang))]
        public string Describe { get; set; }

        [Display(Name = "QQNumber", ResourceType = typeof(one.Res.Lang))]
        public string QQ { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(one.Res.Lang))]
        public string Remark { get; set; }
    }







    public class UserService : UserRepository
    {
        public UserService()
        {
        }




        public void AddUserInfor(string UserId) {

            DataContext.Auth_UserInfor.Add(new Auth_UserInfor() { UserId = UserId });

            DataContext.Commit();
        }



        public IEnumerable<ViewUser> GetAllUsers() {

          var m =   DataContext.Auth_Users.Select(a => new ViewUser()
            {
                UserId = a.UserId,
                UserName = a.UserName,
                Email = a.Email,
                EmailConfirmed = a.EmailConfirmed,
                PhoneNumber = a.PhoneNumber,
                PhoneNumberConfirmed = a.PhoneNumberConfirmed,
                LockoutEnabled = a.LockoutEnabled,
                LockoutEndDateUtc = a.LockoutEndDateUtc,
                AccessFailedCount = a.AccessFailedCount,
                Area = a.Auth_UserInfor.Area,
                Describe = a.Auth_UserInfor.Describe,
                Department = a.Auth_UserInfor.Department,
                Remark = a.Auth_UserInfor.Remark,
                Mobile = a.Auth_UserInfor.Mobile,
                Name = a.Auth_UserInfor.Name,
                QQ = a.Auth_UserInfor.QQ,
                CreateTime = a.CreateTime,
                LastUpdateTime = a.Auth_UserInfor.LastUpdateTime,
               

            }).ToList();
          return m;
        }



        public void updateUserInfo(ref ViewUser viewuser) {

            AutoMapper.IMapper mapper;
            AutoMapper.MapperConfiguration config;
            System.Data.Entity.EntityState entitystate;
            var currentTime = DateTime.Now;

            string guid = string.Empty;

            if (string.IsNullOrEmpty(viewuser.UserId))
            {

                entitystate = System.Data.Entity.EntityState.Added;
                viewuser.UserId = one.Core.Utilities.ShortGuid();
                viewuser.CreateTime = currentTime;

            }
            else 
            {
                entitystate = System.Data.Entity.EntityState.Modified;
                viewuser.LastUpdateTime = currentTime;
            }


            config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<ViewUser, Auth_Users>());
            mapper = config.CreateMapper();
            Auth_Users user = mapper.Map<Auth_Users>(viewuser);

            config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<ViewUser, Auth_UserInfor>());
            mapper = config.CreateMapper();
            Auth_UserInfor userInfo = mapper.Map<Auth_UserInfor>(viewuser);

            DataContext.Auth_Users.Attach(user);
            DataContext.Auth_UserInfor.Attach(userInfo);

            if (entitystate == System.Data.Entity.EntityState.Added)
            {
                DataContext.Entry(user).State = System.Data.Entity.EntityState.Added;
                DataContext.Entry(userInfo).State = System.Data.Entity.EntityState.Added;
            }
            else
            {
                DataContext.Entry(user).Property(s => s.UserName).IsModified = true;
                DataContext.Entry(user).Property(s => s.Email).IsModified = true;
                DataContext.Entry(user).Property(s => s.PhoneNumber).IsModified = true;
                DataContext.Entry(userInfo).Property(a => a.Describe).IsModified = true;
                DataContext.Entry(userInfo).Property(a => a.Mobile).IsModified = true;
                DataContext.Entry(userInfo).Property(a => a.LastUpdateTime).IsModified = true;
                DataContext.Entry(userInfo).Property(a => a.Name).IsModified = true;
                DataContext.Entry(userInfo).Property(a => a.QQ).IsModified = true;
                DataContext.Entry(userInfo).Property(a => a.Remark).IsModified = true;
                DataContext.Entry(userInfo).Property(a => a.Department).IsModified = true;
            }

            DataContext.Commit();
        }




        public void destroyUserInfo(ViewUser viewuser) { 
        
            
            Delete(a=>a.UserId == viewuser.UserId);
            DataContext.Commit();

        }







        public ViewModels.ViewUserProfile getUserSelfProfile(string UserId) {


           var m =   DataContext.Auth_Users.Where(a => a.UserId == UserId)
                .Select(s => new ViewModels.ViewUserProfile()
                {

                    BelongDepartment = s.Auth_UserInfor.Department,
                    UserId = s.UserId,
                    UserName = s.UserName,
                    Email = s.Email,
                    Phone = s.PhoneNumber,
                    Mobile = s.Auth_UserInfor.Mobile,
                    NickName = s.Auth_UserInfor.Name,
                    LastUpdateTime = s.Auth_UserInfor.LastUpdateTime,
                    QQ = s.Auth_UserInfor.QQ,
                    CreateTime = s.CreateTime,

                }).FirstOrDefault();

            return m;

        }



        public void UpdateUserSelfProfile(one.Service.ViewModels.ViewUserProfile model) {

            Auth_Users user = new Auth_Users() {

                UserId = model.UserId,
                Email = model.Email,
                PhoneNumber = model.Phone,
            };

            Auth_UserInfor userinfor = new Auth_UserInfor()
            {
                UserId = model.UserId,
                Name = model.NickName,
                QQ = model.QQ,
                Mobile = model.Mobile,
                LastUpdateTime = DateTime.Now,
                Department = model.Department
                
            };

            user.Auth_UserInfor = userinfor;
            DataContext.Auth_Users.Attach(user);
            DataContext.Entry(user).Property(s => s.Email).IsModified = true;
            DataContext.Entry(user).Property(s =>s.PhoneNumber).IsModified = true;

            DataContext.Auth_UserInfor.Attach(userinfor);
            DataContext.Entry(userinfor).Property(a => a.Department).IsModified = true;
            DataContext.Entry(userinfor).Property(a => a.Name).IsModified = true;
            DataContext.Entry(userinfor).Property(a => a.QQ).IsModified = true;
            DataContext.Entry(userinfor).Property(a => a.Mobile).IsModified = true;
            DataContext.Entry(userinfor).Property(a => a.LastUpdateTime).IsModified = true;

            DataContext.Commit();

        }






        





    }
}
