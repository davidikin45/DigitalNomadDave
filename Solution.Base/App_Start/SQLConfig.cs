using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.App_Start
{
    public static class SQLConfig
    {
        public static void init(string rootApplicationPath)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(rootApplicationPath);
        }
    }
}
