using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace one.Data.Infrastructrue
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
