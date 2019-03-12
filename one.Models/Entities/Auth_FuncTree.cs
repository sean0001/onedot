using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;



namespace one.Data.Entities
{
  public class Auth_FuncTree
    {

      public int Id { get; set; }
      public  int? ParentId { get; set; }
      public  int  ModuleId { get; set; }
      public int FuncTypeId { get; set; } //page or button or auto

     // public byte LevelNumber { get; set; }

      [StringLength(100)]
      public string LevelCode { get; set; }

      [StringLength(100)]
      public string FuncCaption { get; set; }
      [StringLength(100)]
      public string FuncTitle { get; set; }
      [StringLength(100)]
      public string FuncDescribe { get; set; }
      [StringLength(100)]
      public string FuncCaptionEn { get; set; }
      [StringLength(100)]
      public string FuncTitleEn { get; set; }
      [StringLength(100)]
      public string FuncDescribeEn { get; set; }
      [StringLength(100)]
      public string Area { get; set; }
      [StringLength(100)]
      public string Controller { get; set; }
      [StringLength(100)]
      public string Action { get; set; }
      [StringLength(100)]
      public string DefaultParam { get; set; }
      [StringLength(100)]
      public string RequestType { get; set; }
      public int SortNo { get; set; }
      [StringLength(100)]
      public string ImgSrc { get; set; }
      public byte State { get; set; }

      [ForeignKey("ParentId")]
      public virtual Auth_FuncTree Parent { get; set; }

      [ForeignKey("ParentId")]
      public virtual ICollection<Auth_FuncTree> Children { get; set; }

      [ForeignKey("ModuleId")]
      public virtual Pub_Metadata Moudule { get; set; }

      [ForeignKey("FuncTypeId")]
      public virtual Pub_Metadata FuncType { get; set; }

      

    }
}
