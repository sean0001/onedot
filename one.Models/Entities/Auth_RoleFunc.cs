using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace one.Data.Entities
{
  public class Auth_RoleFunc
    {

      public string RoleId { get; set; }
      public int FuncId { get; set; }


      [ForeignKey("RoleId")]
      public virtual Auth_Roles AuthRoles { get; set; }

      [ForeignKey("FuncId")]
      public virtual Auth_FuncTree AuthFuncTree { get; set; }

    }
}
