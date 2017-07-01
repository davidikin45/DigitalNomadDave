using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Implementation.Model
{
    public class EnumDatasourceAttribute : Attribute
    {
        public Type Datasource { get; set; }
    }
}
