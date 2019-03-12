using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace one.Infras.AccessCache
{
    [Serializable]
    public class CachedItem
    {
        public object Item { get; set; }
        public DateTime UtcExpiry { get; set; }
    }

}
