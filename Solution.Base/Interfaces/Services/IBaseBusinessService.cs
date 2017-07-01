using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Solution.Base.Interfaces.UnitOfWork;

namespace Solution.Base.Interfaces.Services
{
    public interface IBaseBusinessService
    {
        IBaseUnitOfWorkScopeFactory UnitOfWorkFactory { get; }
    }
}
