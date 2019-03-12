using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using one.Data.Models;

namespace one.Data
{
   public class RoleFunctionQuery
    {
         public string RoleId { get; set; }
       public string RoleName { get; set; }
       public ICollection<Auth_FuncTree> FuncTree { get; set; }

    }



   
       
   
   
   
   }






