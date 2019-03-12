using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using one.OneDot.ViewModels;

namespace one.OneDot.Repository
{
    public static class SlideRepository
    {

        public static List<SlidesViewModels> GetSlide(string PageSlide) {

            List<SlidesViewModels> svm = new List<SlidesViewModels>() {
                new SlidesViewModels(){
                    Id = 1,
                    Text = "所谓的每股收益应该是基本每股收益：是当期净利润除以当期在外发行的普通股的加权平均来确定，可以反应出来目前股本结构下的盈利能力。",
                    Title = "全屏滑动窗口111",
                    ImgUrl ="Scripts/assets/images/main3.jpg",
                    btns = new List<BtnViewModels>(){
                         new BtnViewModels(){  Caption="动态测试",Title="", Url ="http://www.onedot.red:8008"}
                    }
                }



                //new SlidesViewModels(){
                //    Id = 2,
                //    Text = "所谓的每股收益应该是基本每股收益：是当期净利润除以当期在外发行的普通股的加权平均来确定，可以反应出来目前股本结构下的盈利能力。",
                //    Title = "全屏滑动窗口",
                //    SortNo = 1 ,
                //    ImgUrl ="Scripts/assets/images/main1.jpg",
                //    btns = new List<BtnViewModels>(){
                //         new BtnViewModels(){  Caption="",Title="", Url =""},
                //         new BtnViewModels(){  Caption="",Title="", Url =""}
                //    }
                //},
                //new SlidesViewModels(){
                //    Id = 3,
                //    Text = "所谓的每股收益应该是基本每股收益：是当期净利润除以当期在外发行的普通股的加权平均来确定，可以反应出来目前股本结构下的盈利能力。",
                //    Title = "全屏滑动窗口",
                //    ImgUrl ="Scripts/assets/images/main1.jpg",
                //    btns = new List<BtnViewModels>(){
                //         new BtnViewModels(){  Caption="",Title="", Url =""},
                //         new BtnViewModels(){  Caption="",Title="", Url =""}
                //    }
                //},
                //new SlidesViewModels(){
                //    Id = 4,
                //    Text = "所谓的每股收益应该是基本每股收益：是当期净利润除以当期在外发行的普通股的加权平均来确定，可以反应出来目前股本结构下的盈利能力。",
                //    Title = "全屏滑动窗口",
                //    ImgUrl ="Scripts/assets/images/main1.jpg",
                //    btns = new List<BtnViewModels>(){
                //         new BtnViewModels(){  Caption="",Title="", Url =""},
                //         new BtnViewModels(){  Caption="",Title="", Url =""}
                //    }
                //},

            };
 

            return svm;

        }





    }
}