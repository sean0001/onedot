using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace one.OneDot.ViewModels
{
    public class SlidesViewModels
    {
        public int Id { get; set; }
        public int SortNo { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImgUrl { get; set; }
        public List<BtnViewModels> btns { get; set; }

    }


    public class BtnViewModels {
        public string Caption { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

    }


   


}