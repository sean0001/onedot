using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace one.Service
{

    public class LoggerFileInfo {

        public int RowNo { get; set; }
        public string Name { get; set; }
        public long FileSize { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }

    }




    



  public class LoggerService
    {
        private static readonly string BasePath = ConfigurationManager.AppSettings["LogFilePath"] ;
        private const string InfoSubDirectory = "\\Infor";
        private const string ErrorSubDirectory = "\\Error";
        private const string OperationSubDirectory = "\\Operation";

        private string LoggerPath(Core.Enums.status status) {

            switch (status)
            {

                case Core.Enums.status.Success:
                    return string.Empty;

                case Core.Enums.status.Info:
                    return  BasePath + OperationSubDirectory;

                case Core.Enums.status.Warning:
                    return  BasePath + InfoSubDirectory;

                case Core.Enums.status.Error:
                    return BasePath + ErrorSubDirectory;

                default:
                    return string.Empty;

            }
             
        }


        public List<LoggerFileInfo> GetLogFiles(Core.Enums.status status) {

            int rowNo = 1;
            List <LoggerFileInfo> files = new List<LoggerFileInfo>();
            DirectoryInfo dir = new DirectoryInfo(LoggerPath(status)); 
            FileInfo[] info = dir.GetFiles();
            foreach (FileInfo f in info)
            {

                var logfile = new LoggerFileInfo() {
                    RowNo = rowNo ++,
                    Name = f.Name,
                    FileSize = f.Length,
                    LastAccessTime = f.LastAccessTime,
                    LastWriteTime = f.LastWriteTime,
                    CreateTime = f.CreationTime,
                    
                };
                files.Add(logfile);
            }

            return files;
        }






        public string ReadLoggerFile(string Name, Core.Enums.status status) {

            string file = LoggerPath(status)+ "\\"+ Name;
            return File.ReadAllText( file , System.Text.Encoding.Default);

        }



        public void ClearAllLogger(Core.Enums.status status) {

            List<LoggerFileInfo> files = new List<LoggerFileInfo>();
            DirectoryInfo dir = new DirectoryInfo(LoggerPath(status));
            FileInfo[] info = dir.GetFiles();
            foreach (FileInfo f in info)
            {
                f.Delete();
            }
        }



    }
}
