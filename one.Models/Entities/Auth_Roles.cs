using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace one.Data.Entities
{
  public class Auth_Roles : IdentityRole<string,Auth_UserRole>
    {

     public Auth_Roles() {
         this.Id = one.Core.Utilities.ShortGuid();
      }
     public virtual string Describe { get; set; }

        public virtual string NameEn { get; set; }

        public virtual string DescribeEn { get; set; }



    }
}
