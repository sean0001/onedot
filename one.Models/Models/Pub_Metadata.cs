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
    
    public partial class Pub_Metadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pub_Metadata()
        {
            this.Auth_FuncTree = new HashSet<Auth_FuncTree>();
            this.Auth_FuncTree1 = new HashSet<Auth_FuncTree>();
        }
    
        public int Id { get; set; }
        public byte CategoryId { get; set; }
        public string Title { get; set; }
        public string Describe { get; set; }
        public string Value { get; set; }
        public byte State { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Auth_FuncTree> Auth_FuncTree { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Auth_FuncTree> Auth_FuncTree1 { get; set; }
        public virtual Pub_MetaIndex Pub_MetaIndex { get; set; }
    }
}
