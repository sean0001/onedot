using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace one.Core
{

    public static class SysVariant
    {

        /// <summary>
        ///  get the logger saving  mode, optional is 'TXT' or 'DB'
        /// </summary>
      //  public static readonly string LoggerSaveMode = System.Configuration.ConfigurationManager.AppSettings["LoggerSaveMode"].ToUpper();



        /// <summary>
        /// get the save path of logger. example : "c:\user\logger"  or "logger" , the "logger" default parent Directory  i s  Web root ;
        /// </summary>
        public static readonly string LogFilePath = string.Concat(System.Configuration.ConfigurationManager.AppSettings["LogFilePath"].TrimEnd('\\'), "\\");


        /// <summary>
        /// 分割制表符
        /// </summary>
        public const string LineSplit = " - ";



        /// <summary>
        /// 换行回车符
        /// </summary>
        public const string LineBreak = "\r\n";



        /// <summary>
        /// webroot的物理路径
        /// </summary>
        public static readonly string ServerPath = AppDomain.CurrentDomain.BaseDirectory;



        /// <summary>
        /// 
        /// </summary>
        public static string WebRoot;

    }

}
