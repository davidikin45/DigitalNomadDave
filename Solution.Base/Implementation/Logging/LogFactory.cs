using NLog;
using Solution.Base.Interfaces.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Implementation.Logging
{
    public class LogFactory : Interfaces.Logging.ILogFactory
    {
        private static ConcurrentDictionary<string, Interfaces.Logging.ILogger> logs = new ConcurrentDictionary<string, Interfaces.Logging.ILogger>();

        public Interfaces.Logging.ILogger GetLogger(string name = "ApplicationLog")
        {
            Interfaces.Logging.ILogger logger = GetLoggerInner(name);
            return logger;
        }

        private Interfaces.Logging.ILogger GetLoggerInner(string name)
        {
            Interfaces.Logging.ILogger logger;
            if (!logs.ContainsKey(name))
            {
                logger = (Interfaces.Logging.ILogger)LogManager.GetLogger(name, typeof(Logger));
                logger = logs.GetOrAdd(name, logger);
            }
            else
            {
               logs.TryGetValue(name, out logger);
            }
            return logger;
        }

    }
}
