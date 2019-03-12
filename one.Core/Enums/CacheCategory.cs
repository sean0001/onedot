using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace one.Core.Enums
{
    public enum CacheCategory
    {
        None = 0, //缺省
        STD  = 1, //System cache 
        OPC = 2, //OnePageCache,
        OSC = 3  //OneSecondDataCache
    }
}
