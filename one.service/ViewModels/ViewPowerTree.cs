using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace one.Service.ViewModels
{

    public class ViewPowerTree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string ImgSrc { get; set; }
        public Boolean HasChildren { get; set; }
        public IEnumerable<ViewPowerTree> treeNode { get; set; }
        public string Title { get; set; }
        public Boolean Checked { get; set; }

    }








}
