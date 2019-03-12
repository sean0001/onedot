using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace one.Service
{
    public class OSelectListItem
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public string Describe { get; set; }
        public bool Disabled { get; set; }
        public bool Selected { get; set; }
        public OSelectListItem() { }
    }
}
