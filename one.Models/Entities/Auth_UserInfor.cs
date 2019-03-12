using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;



namespace one.Data.Entities
{
   public class Auth_UserInfor
    {
       [Key, ForeignKey("AuthUser")]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Area { get; set; }
        public string Describe { get; set; }
        public string Remark { get; set; }
        public DateTime? LastUpdateTime { get; set; }

        public string Mobile { get; set; }

        public string QQ { get; set; }

        public virtual Auth_Users AuthUser { get; set; }

    }
}
