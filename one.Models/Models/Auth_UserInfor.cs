//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace one.Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Auth_UserInfor
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Area { get; set; }
        public string Describe { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public string Mobile { get; set; }
        public string QQ { get; set; }
    
        public virtual Auth_Users Auth_Users { get; set; }
    }
}
