using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using one.Data.Entities;
using System.Data.Objects;

namespace one.Data
{
    public class DBInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {


        private static readonly string con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

       // private readonly string[] Tables = { "Auth_Function", "Auth_AppModules", "Auth_Role", "Auth_RoleFunction", "Auth_UserRole", "Auth_Users" };

        protected override void Seed(ApplicationDbContext context)
        {
            IniAuthDatabase(context);
            base.Seed(context);
        }





        /// <summary>
        /// 初始化权限数据库内容
        /// </summary>
        /// <param name="context"></param>
        private void IniAuthDatabase(ApplicationDbContext context)
        {

            context.Database.ExecuteSqlCommand("create unique index IX_AuthRoles_Name on dbo.Auth_Roles (Name ASC)");
            //context.Database.ExecuteSqlCommand("create unique index IX_Auth_Users_UserName on dbo.Auth_Users (UserName ASC)");
            //context.Database.ExecuteSqlCommand("create unique index IX_Auth_Users_Email on dbo.Auth_Users (Email ASC)");
           

            



            var AuthRole = new List<Auth_Roles>(){

                new Auth_Roles(){ Id ="1111111111" ,Name = "超级管理员"},
                new Auth_Roles(){ Id ="2222222222" ,Name = "系统管理员"},
                new Auth_Roles(){ Id ="3333333333" ,Name = "客户经理"},
                new Auth_Roles(){ Id ="4444444444" ,Name = "产品经理"},

            };
            AuthRole.ForEach(a => context.Roles.Add(a));




            var MetaIndex = new List<Pub_MetaIndex>(){
                new Pub_MetaIndex(){ Id = 1, CategoryName = "系统模块" , Describe ="系统模块"},
                new Pub_MetaIndex(){ Id = 2, CategoryName = "操作功能" , Describe ="操作功能"},
                new Pub_MetaIndex(){ Id = 3, CategoryName = "网络请求类型" , Describe ="网络请求类型"}
            };
            MetaIndex.ForEach(a => context.Pub_MetaIndex.Add(a));




            var MetaData = new List<Pub_Metadata>(){
                new Pub_Metadata(){Id = 1, CategoryId = 1, Title = "系统管理", Describe = "系统管理", Value = "" , State = 1},
                new Pub_Metadata(){Id = 2, CategoryId = 1, Title = "用户管理", Describe = "用户管理", Value = "" , State = 1},
                new Pub_Metadata(){Id = 3, CategoryId = 2, Title = "功能树根", Describe = "功能树根", Value = "" , State = 1},
                new Pub_Metadata(){Id = 4, CategoryId = 2, Title = "Module", Describe = "模块", Value = "" , State = 1},
                new Pub_Metadata(){Id = 5, CategoryId = 2, Title = "Page", Describe = "页面", Value = "" , State = 1},
                new Pub_Metadata(){Id = 6, CategoryId = 2, Title = "Btn", Describe = "按钮", Value = "" , State = 1},
                new Pub_Metadata(){Id = 7, CategoryId = 2, Title = "Req", Describe = "Ajax请求", Value = "" , State = 1},
                 new Pub_Metadata(){Id = 8, CategoryId = 3, Title = "Post", Describe = "Post", Value = "" , State = 1},
                new Pub_Metadata(){Id = 9, CategoryId = 3, Title = "Get", Describe = "Get", Value = "" , State = 1},
                new Pub_Metadata(){Id = 10, CategoryId =3, Title = "Ajax", Describe = "Ajax", Value = "" , State = 1}
            };
            MetaData.ForEach(a => context.Pub_Metadata.Add(a));

            context.SaveChanges();


            var FuncTree = new List<Auth_FuncTree>(){
            
                new Auth_FuncTree(){Id = 1, ParentId = null, ModuleId = 1, FuncTypeId =3, FuncCaption="root",FuncTitle="root",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
                new Auth_FuncTree(){Id = 2, ParentId = 1, ModuleId = 1, FuncTypeId = 4, FuncCaption="系统管理模块",FuncTitle="系统管理模块",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
                new Auth_FuncTree(){Id = 3, ParentId = 2, ModuleId = 1, FuncTypeId = 5, FuncCaption="用户管理页面",FuncTitle="用户管理页面",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
                new Auth_FuncTree(){Id = 4, ParentId = 3, ModuleId = 1, FuncTypeId = 6, FuncCaption="修改按钮",FuncTitle="修改按钮",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
                new Auth_FuncTree(){Id = 5, ParentId = 3, ModuleId = 1, FuncTypeId = 6, FuncCaption="保存按钮",FuncTitle="保存按钮",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },

                 new Auth_FuncTree(){Id = 6, ParentId = 1, ModuleId = 1, FuncTypeId =4, FuncCaption="财务分析模块",FuncTitle="财务分析模块",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
                new Auth_FuncTree(){Id = 7, ParentId = 6, ModuleId = 1, FuncTypeId = 5, FuncCaption="录入凭证页面",FuncTitle="录入凭证页面",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
                new Auth_FuncTree(){Id = 8, ParentId = 7, ModuleId = 1, FuncTypeId = 6, FuncCaption="新增凭证按钮",FuncTitle="新增凭证按钮",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
                new Auth_FuncTree(){Id = 9, ParentId = 7, ModuleId = 1, FuncTypeId = 6, FuncCaption="保存凭证按钮",FuncTitle="保存凭证按钮",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
                new Auth_FuncTree(){Id = 10, ParentId = 7, ModuleId = 1, FuncTypeId = 7, FuncCaption="刷新凭证ajax",FuncTitle="刷新凭证ajax",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },

                 new Auth_FuncTree(){Id = 11, ParentId = 6, ModuleId = 1, FuncTypeId = 5, FuncCaption="主管复核页面",FuncTitle="主管复核页面",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
                new Auth_FuncTree(){Id = 12, ParentId = 11, ModuleId = 1, FuncTypeId = 6, FuncCaption="同意复核按钮",FuncTitle="同意复核按钮",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
                new Auth_FuncTree(){Id = 13, ParentId = 11, ModuleId = 1, FuncTypeId = 6, FuncCaption="取消复核按钮",FuncTitle="取消复核按钮",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
                new Auth_FuncTree(){Id = 14, ParentId = 11, ModuleId = 1, FuncTypeId = 7, FuncCaption="刷新ajax",FuncTitle="刷新ajax",FuncDescribe ="",
                FuncCaptionEn ="System Manager Operation", FuncTitleEn = "To Manager System Operation Authentication",FuncDescribeEn ="",
                Area ="System", Controller="Manager",Action = "PostInfor", DefaultParam="",RequestType="",SortNo=100,ImgSrc="",State = 1
                },
            };

            
            FuncTree.ForEach(a => context.Auth_FuncTree.Add(a));



            context.SaveChanges();


            Auth_UserRole _userrole;
            var _user = new Auth_Users()
            {
                Id = "ae4bc42b88b254ab",
                Email = "seanmoo@sina.com",
                EmailConfirmed = false,
                PasswordHash = "APMVh184JUwaMJ30+/iNW2ILlBvS0tbLDgQXKrproj9/v5ixdbUuwUNQxGdc/t5sMQ==",
                SecurityStamp = "7a610ef7-eca3-471d-8a18-69c8312b1b9a"
            };

            _userrole = new Auth_UserRole() { RoleId = "2222222222", UserId = "ae4bc42b88b254ab" };
            _user.Roles.Add(_userrole);

            _userrole = new Auth_UserRole() { RoleId = "3333333333", UserId = "ae4bc42b88b254ab" };
            _user.Roles.Add(_userrole);

            _userrole = new Auth_UserRole() { RoleId = "4444444444", UserId = "ae4bc42b88b254ab" };
            _user.Roles.Add(_userrole);

            context.Users.Add(_user);

            
            
            context.Auth_RoleFunc.Add(new Auth_RoleFunc() { RoleId = "2222222222", FuncId = 2 });
            context.Auth_RoleFunc.Add(new Auth_RoleFunc() { RoleId = "2222222222", FuncId = 3 });
            context.Auth_RoleFunc.Add(new Auth_RoleFunc() { RoleId = "2222222222", FuncId = 4 });


            context.Auth_RoleFunc.Add(new Auth_RoleFunc() { RoleId = "3333333333", FuncId = 2 });
            context.Auth_RoleFunc.Add(new Auth_RoleFunc() { RoleId = "3333333333", FuncId = 3 });
            context.Auth_RoleFunc.Add(new Auth_RoleFunc() { RoleId = "3333333333", FuncId = 5 });

            context.SaveChanges();

















        }









        /// <summary>
        /// 初始化流程数据库内容
        /// </summary>
        /// <param name="context"></param>
        private void IniFlowDatabase(ApplicationDbContext context)
        {




        }




    }

}
