using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using one.Data;
using one.Data.Models;
using one.Data.Repository;
using System.ComponentModel.DataAnnotations;
using System.Transactions;


namespace one.Service
{



    public class MenuTree {
        public int? Id { get; set; }

        [Display(Name = "ParentId", ResourceType = typeof(one.Res.Lang))]
        public Nullable<int> ParentId { get; set; }

        [UIHint("GridForeignKey")]
        public int ModuleId { get; set; }

        [Display(Name = "ModuleName", ResourceType = typeof(one.Res.Lang))]
        public string ModuleName { get; set; }

        [UIHint("GridForeignKey")]
        public int FuncTypeId { get; set; }

        [Display(Name = "FuncCaption", ResourceType = typeof(one.Res.Lang))]
        public string FuncCaption { get; set; }

        [Display(Name = "FuncTitle", ResourceType = typeof(one.Res.Lang))]
        public string FuncTitle { get; set; }

        [Display(Name = "FuncDescribe", ResourceType = typeof(one.Res.Lang))]
        public string FuncDescribe { get; set; }
        public string FuncCaptionEn { get; set; }
        public string FuncTitleEn { get; set; }
        public string FuncDescribeEn { get; set; }

        [Display(Name = "RouteArea", ResourceType = typeof(one.Res.Lang))]
        public string Area { get; set; }

        [Display(Name = "RouteController", ResourceType = typeof(one.Res.Lang))]
        public string Controller { get; set; }

        [Display(Name = "RouteAction", ResourceType = typeof(one.Res.Lang))]
        public string Action { get; set; }

        [Display(Name = "RouteDefaultParam", ResourceType = typeof(one.Res.Lang))]
        public string DefaultParam { get; set; }

        [Display(Name = "NetRequestType", ResourceType = typeof(one.Res.Lang))]
        [UIHint("GridForeignKey")]
        public string RequestType { get; set; }

        [Display(Name = "SortNo", ResourceType = typeof(one.Res.Lang))]
        public int SortNo { get; set; }

        [Display(Name = "ImgScr", ResourceType = typeof(one.Res.Lang))]
        public string ImgSrc { get; set; }

        [Display(Name = "State", ResourceType = typeof(one.Res.Lang))]
        public byte State { get; set; }
        public List<MenuTree> TreeNode { get; set; }

        public bool Checked { get; set; }
        
    
    }







    public class ViewAuthUser {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public virtual IEnumerable<ViewUserRole> Auth_Roles { get; set; }
    
    }





    public class ViewUserRole {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public IEnumerable<ViewFunc> FuncTree { get; set; }
    }



    public class ViewRoleFunction
    {

        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<ViewFunc> FuncTree { get; set; }
    }




    public class ViewFunc {
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int FuncTypeId { get; set; }
        public string FuncCaption { get; set; }
        public string FuncTitle { get; set; }
        public string FuncDescribe { get; set; }
        public string FuncCaptionEn { get; set; }
        public string FuncTitleEn { get; set; }
        public string FuncDescribeEn { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string DefaultParam { get; set; }
        public string RequestType { get; set; }
        public int SortNo { get; set; }
        public string ImgSrc { get; set; }
        public byte State { get; set; }
        public IEnumerable<ViewFunc> Nodes { get; set; }
    
    }

  





   public class PowerService : RoleRepository
    {

       private static List<Auth_FuncTree> _FuncTree { get; set; }
       
       private static List<MenuTree> _MenuTree { get; set; }


        /// <summary>
        /// 树结构菜单数据
        /// </summary>
      public List<MenuTree> MenuTree
        {
            get
            {
                if (_MenuTree == null)
                    _MenuTree = GetTree(FuncTree, 1);
                return _MenuTree;
                //return GetTree(FuncTree, 1);
            }
        }

        /// <summary>
        /// 列表结构菜单数据
        /// </summary>
        public IEnumerable<Auth_FuncTree> FuncTree
       {
           get
           {
                return one.Infras.DataCache.SecondCache.GetCaheEntity<IEnumerable<Auth_FuncTree>>(GetFuncTree,"FuncTree");
                //return GetFuncTree();
               
            }
       }




        private IEnumerable<Auth_FuncTree> GetFuncTree() {

            return DataContext.Auth_FuncTree.ToList();

        }




        public List<MenuTree> GetTreeList() {

          return   DataContext.Auth_FuncTree
                .Select(s => new MenuTree()
                {
                    Id = s.Id,
                    Area = s.Area,
                    ModuleId = s.ModuleId,
                    Controller = s.Controller,
                    ParentId = s.ParentId,
                    Action  = s.Action,
                    DefaultParam = s.DefaultParam,
                    FuncCaption = s.FuncCaption,
                    FuncCaptionEn = s.FuncCaptionEn,
                    FuncDescribe = s.FuncDescribe,
                    FuncDescribeEn = s.FuncDescribeEn,
                    FuncTitle = s.FuncTitle,
                    FuncTitleEn = s.FuncTitleEn,
                    ImgSrc = s.ImgSrc,
                    ModuleName = s.Pub_Metadata1.Title,
                    SortNo = s.SortNo,
                    RequestType = s.RequestType,
                    State = s.State,
                    FuncTypeId = s.FuncTypeId
                }).ToList();
        }




       public List<MenuTree>  GetTree(IEnumerable<Auth_FuncTree>  List,  int? parent)
       {
           return List.Where(x => x.ParentId == parent)
               .Select(x => new MenuTree
               {
                   Id = x.Id,
                   ParentId = x.ParentId,
                   FuncTitle = x.FuncTitle,
                   FuncTitleEn = x.FuncTitleEn,
                   FuncCaption = x.FuncCaption,
                   FuncCaptionEn = x.FuncCaptionEn,
                   FuncDescribe = x.FuncDescribe,
                   FuncDescribeEn = x.FuncDescribeEn,
                   ModuleId = x.ModuleId,
                   ModuleName = x.Pub_Metadata1.Title,
                   FuncTypeId = x.FuncTypeId,
                   Area = x.Area,
                   Controller = x.Controller,
                   Action = x.Action,
                   DefaultParam = x.DefaultParam,
                   ImgSrc = x.ImgSrc,
                   RequestType = x.RequestType,
                   SortNo = x.SortNo,
                   State = x.State,
                   TreeNode = GetTree(List, x.Id)
               }).ToList();
       }



       public List<MenuTree> GetTree() {
           var tree =  GetTree(FuncTree, 1);
           return tree;
       
       }




       public List<ViewRoleFunction> GetRoleMenuTree(string[] Roles) {


           return DataContext.Auth_Roles
                .Select(a => new ViewRoleFunction()
           {
               RoleId = a.RoleId,
               RoleName = a.Name,
               FuncTree = a.Auth_FuncTree.Select(x => new ViewFunc()
               {
                   Id = x.Id,
                   ParentId = x.ParentId,
                   FuncTitle = x.FuncTitle,
                   FuncTitleEn = x.FuncTitleEn,
                   FuncCaption = x.FuncCaption,
                   FuncCaptionEn = x.FuncCaptionEn,
                   FuncDescribe = x.FuncDescribe,
                   FuncDescribeEn = x.FuncDescribeEn,
                   ModuleId = x.ModuleId,
                   ModuleName = x.Pub_Metadata1.Title,
                   FuncTypeId = x.FuncTypeId,
                   Area = x.Area,
                   Controller = x.Controller,
                   Action = x.Action,
                   DefaultParam = x.DefaultParam,
                   ImgSrc = x.ImgSrc,
                   RequestType = x.RequestType,
                   SortNo = x.SortNo,
                   State = x.State,
               })
           }).ToList();



           
       }



        public List<MenuTree> GetRolesModuleMenuTree(string RoleId, int ModuleId) {

            var menu = FuncTree.Where(s => s.ModuleId == ModuleId).ToList();


            var roleMenu = DataContext.Auth_FuncTree
                .Where(s => s.Auth_Roles.Any(ss => ss.RoleId == RoleId))
                .Select(x => new MenuTree() {
                    Id = x.Id,
                }).ToList();


            foreach (var item in menu)
            {
                if (roleMenu.Any(s => s.Id == item.Id))
                    item.State = 100;
                else
                    item.State = 0;
            }

            if (menu.Count == 0) {
                return new List<MenuTree>();
            }

            var m = GetTree(menu, menu.Min(ss => ss.Id));
            return m;
        }


    

       public List<MenuTree> GetUserMenuTree(string UserId) {

           var bbc = DataContext.Auth_Users
                .Where(s => s.LockoutEnabled == false)
               .Select(user => new ViewAuthUser()
               {
                   UserId = user.UserId,
                   Auth_Roles = user.Auth_Roles
                   .Select(roles => new ViewUserRole()
                   {
                       RoleId = roles.RoleId,
                       UserId = roles.Name,
                       FuncTree = roles.Auth_FuncTree
                       .Select(tree => new ViewFunc()
                       {
                           Id = tree.Id,
                           ParentId = tree.ParentId,
                           ModuleId = tree.ModuleId,
                           Area = tree.Area,
                           Action = tree.Action,
                           Controller = tree.Controller,
                           DefaultParam = tree.DefaultParam
                       })
                   })
               }).ToList();

           var mmmm = DataContext.Auth_Roles.SelectMany(a => a.Auth_FuncTree, (p, c) => new { p.Name, p.RoleId, c.Id, c.Action, c.Area }).ToList();

           //myResume = _dbContext.Set<MyUser>()
           //     .Include("Resumes")
           //     .Include("Jobs")
           //     .Include("Projects")
           //     .Include("Works")
           //     .Single(u => u.UserId == uid);

           //context.Entry(blog) 
           //.Collection("Posts") 
           //.Query() 
           //.Where(p => p.Tags.Contains("entity-framework") 
           //.Load(); 

           //var m = datacontext.auth_roles.where(s => s.auth_users.any(a => a.userid == userid)).tolist();
           //var sm = datacontext.auth_roles.where(ss => ss.auth_users.any(s => s.userid == userid)).selectmany(ss => ss.auth_functree.tolist()).tolist();
           //var bb = datacontext.auth_roles.select(a => a.auth_functree).tolist();
           //var cc = datacontext.auth_users.select(a => a.auth_roles).tolist();
           //var ccc = datacontext.auth_users.selectmany(a => a.auth_roles).tolist();

           var cccc = DataContext.Auth_Users;
           return null;
       }





        public void UpdateFuncTree(ref MenuTree mt) {

            AutoMapper.IMapper mapper;
            AutoMapper.MapperConfiguration config;

            config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<MenuTree, Auth_FuncTree>());
            mapper = config.CreateMapper();
            Auth_FuncTree tree = mapper.Map<Auth_FuncTree>(mt);

            tree.State = 1;

            DataContext.Auth_FuncTree.Attach(tree);
            DataContext.Entry(tree).State = getEntitystate(mt.Id);
            DataContext.Commit();
            if (mt.Id == null) {
                mt.Id = tree.Id;
            }
        }





        public void UpdateRolePrivilege(int moduleId,string roleId,string[] menu) {


            string sqladd = @"insert into Auth_RoleFunc(RoleId,FuncId) values({0},{1})";
            string sqldel = @"delete a from Auth_RoleFunc a
inner join Auth_FuncTree b on a.FuncId = b.Id
inner join Auth_Roles c on a.RoleId = c.RoleId
where a.RoleId = {0} and b.ModuleId = {1} ";


            using (TransactionScope ts = new TransactionScope()) {

                DataContext.Database.ExecuteSqlCommand(sqldel, roleId, moduleId);

                if (menu != null) {

                    for (int i = 0; i < menu.Length; i++)
                    {
                        DataContext.Database.ExecuteSqlCommand(sqladd, roleId, menu[i]);
                    }
                }
                DataContext.Commit(ts);
            }

 
        }




    }
}
