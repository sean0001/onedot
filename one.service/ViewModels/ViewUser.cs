using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace one.Service.ViewModels
{


    

    public class ViewUserProfile
    {
//        [HiddenInput(DisplayValue = true)]
        public string UserId { get; set; }

  //      [HiddenInput(DisplayValue = true)]
        public string UserName { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public string Mobile { get; set; }
        public string Department { get; set; }

        public string QQ { get; set; }

    //    [HiddenInput(DisplayValue = true)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? CreateTime { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? LastUpdateTime { get; set; }

        public string BelongDepartment { get; set; }

    }
}
