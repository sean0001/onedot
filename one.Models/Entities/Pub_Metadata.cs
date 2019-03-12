using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;



namespace one.Data.Entities
{
    public class Pub_Metadata
    {




        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public byte CategoryId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Describe { get; set; }

        [StringLength(100)]
        public string Value { get; set; }

        public byte State { get; set; }

        [ForeignKey("CategoryId")]
        public Pub_MetaIndex Pub_MetaIndex_ { get; set; }
        public ICollection<Auth_FuncTree> Modules { get; set; }
        public ICollection<Auth_FuncTree> FuncTypes { get; set; }

    }
}
