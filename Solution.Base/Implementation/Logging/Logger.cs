using NLog;
using Solution.Base.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Implementation.Logging
{
    public class Logger : NLog.Logger, Interfaces.Logging.ILogger
    {
       public new void Debug(Exception exception, string format, params object[] args)
        {
            if (!base.IsDebugEnabled) return;
            var logEvent = GetLogEvent(this.Name, LogLevel.Debug, exception, format, args);
            base.Log(typeof(Logger), logEvent);
        }

        public new void Error(Exception exception, string format, params object[] args)
        {
            if (!base.IsErrorEnabled) return;
            var logEvent = GetLogEvent(this.Name, LogLevel.Error, exception, format, args);
            base.Log(typeof(Logger), logEvent);
        }

        public new void Fatal(Exception exception, string format, params object[] args)
        {
            if (!base.IsFatalEnabled) return;
            var logEvent = GetLogEvent(this.Name, LogLevel.Fatal, exception, format, args);
            base.Log(typeof(Logger), logEvent);
        }

        public new void Info(Exception exception, string format, params object[] args)
        {
            if (!base.IsInfoEnabled) return;
            var logEvent = GetLogEvent(this.Name, LogLevel.Info, exception, format, args);
            base.Log(typeof(Logger), logEvent);
        }

        public new void Trace(Exception exception, string format, params object[] args)
        {
            if (!base.IsTraceEnabled) return;
            var logEvent = GetLogEvent(this.Name, LogLevel.Trace, exception, format, args);
            base.Log(typeof(Logger), logEvent);
        }

        public new void Warn(Exception exception, string format, params object[] args)
        {
            if (!base.IsWarnEnabled) return;
            var logEvent = GetLogEvent(this.Name, LogLevel.Warn, exception, format, args);
            base.Log(typeof(Logger), logEvent);
        }

        public void Debug(Exception exception)
        {
            this.Debug(exception, string.Empty);
        }

        public void Error(Exception exception)
        {
            this.Error(exception, string.Empty);
        }

        public void Fatal(Exception exception)
        {
            this.Fatal(exception, string.Empty);
        }

        public void Info(Exception exception)
        {
            this.Info(exception, string.Empty);
        }

        public void Trace(Exception exception)
        {
            this.Trace(exception, string.Empty);
        }

        public void Warn(Exception exception)
        {
            this.Warn(exception, string.Empty);
        }

        private LogEventInfo GetLogEvent(string loggerName, LogLevel level, Exception exception, string format, object[] args)
        {
            string assemblyProp = string.Empty;
            string classProp = string.Empty;
            string methodProp = string.Empty;
            string messageProp = string.Empty;
            string stackTraceProp = string.Empty;
            string innerMessageProp = string.Empty;
            string typeProp = string.Empty;

            var logEvent = new LogEventInfo
                (level, loggerName, string.Format(format, args));

            logEvent.Exception = exception;

            if (exception != null)
            {
                assemblyProp = exception.Source;
                classProp = exception.TargetSite.DeclaringType.FullName;
                if (exception.TargetSite.DeclaringType.DeclaringType != null)
                {
                    classProp = exception.TargetSite.DeclaringType.DeclaringType.FullName;
                }
                methodProp = exception.TargetSite.Name;
                stackTraceProp = exception.StackTrace;
                messageProp = exception.Message;
                typeProp = exception.GetType().ToString();

                if (exception.InnerException != null)
                {
                    innerMessageProp = exception.InnerException.Message;
                }
            }

            logEvent.Properties["type"] = typeProp;
            logEvent.Properties["assembly"] = assemblyProp;
            logEvent.Properties["class"] = classProp;
            logEvent.Properties["method"] = methodProp;
            logEvent.Properties["stackTrace"] = stackTraceProp;
            logEvent.Properties["exceptionMessage"] = messageProp;
            logEvent.Properties["innerExceptionMessage"] = innerMessageProp;

            return logEvent;
        }
    }
}
