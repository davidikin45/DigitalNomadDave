using Solution.Base.Implementation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Interfaces.Model
{
    public interface ICurrentUser
    {
        User User { get; }
    }
}
