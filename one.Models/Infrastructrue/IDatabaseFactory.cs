using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using one.Data.Models;

namespace one.Data.Infrastructrue
{
    public interface IDatabaseFactory : IDisposable
    {
        OneDotEntities  Get();
    }
}
