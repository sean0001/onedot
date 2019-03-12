using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using one.Core.Enums;
using one.Infras.DataCache;


namespace one.Service

{





    /// <summary>
    /// 记录系统的基础通用 列表 字符串。
    /// </summary>
    public partial class CommonData
    {

        //private static IEnumerable<OSelectListItem> roles;
        public static IEnumerable<OSelectListItem> Roles
        {
            get
            {
                    return  SecondCache.GetCaheEntity<IEnumerable<OSelectListItem>>(new RoleService().GetRolesSelectList);
            }
        }





       // private static  IEnumerable<OSelectListItem> netRequestType;
        public static IEnumerable<OSelectListItem> NetRequestType
        {
            get
            {
               
                    //netRequestType = new ApplicationConfigService().GetApplicationConfig(ApplicationMetaDataCategory.NetRequestType);

                    return  SecondCache.GetCaheEntity<IEnumerable<OSelectListItem>>(
                        new ApplicationConfigService().GetApplicationConfig, 
                        "", 
                        ApplicationMetaDataCategory.NetRequestType);


            }
        }



        // private  static  IEnumerable<OSelectListItem> applicationModule;

        public static IEnumerable<OSelectListItem> ApplicationModule
        {
            get
            {
                    //applicationModule = new ApplicationConfigService().GetApplicationConfig(ApplicationMetaDataCategory.ApplicationModule);

                return  SecondCache.GetCaheEntity<IEnumerable<OSelectListItem>>(
                        new ApplicationConfigService().GetApplicationConfig, 
                        "", 
                        ApplicationMetaDataCategory.ApplicationModule);

            }
        }



        //private static IEnumerable<OSelectListItem> powerFunctionLevel;

        public static IEnumerable<OSelectListItem> PowerFunctionLevel
        {
            get
            {
                
                    //powerFunctionLevel = new ApplicationConfigService().GetApplicationConfig(ApplicationMetaDataCategory.PowerFunctionLevel);

                return  SecondCache.GetCaheEntity<IEnumerable<OSelectListItem>>(
                        new ApplicationConfigService().GetApplicationConfig,
                        "",
                        ApplicationMetaDataCategory.PowerFunctionLevel);

                

            }
        }



    }
}
