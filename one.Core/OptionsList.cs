using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace one.Core
{
    [DataContract(Name = "OptionsList", Namespace = "")]
    public class OptionsList : List<string>
    {
        public override string ToString()
        {
            var optionsString = new StringBuilder();
            foreach (var option in this)
            {
                optionsString.Append(option);
            }
            return optionsString.ToString();
        }
    }
}
