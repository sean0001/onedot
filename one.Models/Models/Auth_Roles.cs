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
    
    public partial class Auth_Roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Auth_Roles()
        {
            this.Auth_FuncTree = new HashSet<Auth_FuncTree>();
            this.Auth_Users = new HashSet<Auth_Users>();
        }
    
        public string RoleId { get; set; }
        public string Describe { get; set; }
        public string NameEn { get; set; }
        public string DescribeEn { get; set; }
        public string Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Auth_FuncTree> Auth_FuncTree { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Auth_Users> Auth_Users { get; set; }
    }
}
