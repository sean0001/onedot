using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace one.Data.Entities
{
   public class Pub_MetaIndex
    {

      [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public byte Id { get; set; }

       [StringLength(50)]
       public string CategoryName { get; set; }
       [StringLength(120)]
       public string Describe { get; set; }


    }
}
