using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace one.Core
{
  public  class Utilities
    {

        public const string ShowSuccess = "<script>toastr.success(onedot.gw('operationSuccess'));</script>";
        public const string ShowError = "<script>toastr.error(onedot.gw('operationFailure'));</script>";

        public static string ShortGuid()
      {
          long i = 1;
          foreach (byte b in Guid.NewGuid().ToByteArray())
          {
              i *= ((int)b + 1);
          }

          return string.Format("{0:x}", i - DateTime.Now.Ticks);
      }








    }
}
