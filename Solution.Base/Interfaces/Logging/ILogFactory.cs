﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Interfaces.Logging
{
    public interface ILogFactory
    {
        Interfaces.Logging.ILogger GetLogger(string name = "ApplicationLog");
    }
}
