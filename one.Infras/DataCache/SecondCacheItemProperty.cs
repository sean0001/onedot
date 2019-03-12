using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace one.Infras.DataCache
{
    public class SecondCacheItemProperty
    {
        public string Url { get; set; }
        public byte UsageBucket { get; set; }
        public DateTime utcExpires { get; set; }
        public DateTime UtcCreated { get; set; }
        public DateTime UtcLastUsageUpdate { get; set; }
        public string TimeSpan { get; set; }

    }
}
