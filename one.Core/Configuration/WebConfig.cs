using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace one.Core
{


    /// <summary>
    /// read configuration parameters  from Web.config.
    /// </summary>
    public class WebConfig
    {
        public static string Email;

        static WebConfig() {

            Email = ConfigurationManager.AppSettings[""];

        }




    }
}
