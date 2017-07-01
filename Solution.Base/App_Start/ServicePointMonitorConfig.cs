using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Reflection;
using Console = System.Console;
using Solution.Base.Interfaces.Logging;
using Solution.Base.Implementation.Logging;
using System.Net.Sockets;

namespace Solution.Base.App_Start
{
    public class ServicePointMonitor
    {
        public static int DefaultConnectionLeaseTimeout = -1;
        public static TimeSpan DefaultRequestTimeout = new TimeSpan(0, 1, 40);
        private Action<string> _log;

        public ServicePointMonitor(Action<string> log)
        {
            _log = log;
        }

        private void Print()
        {
            Output("-- BEGIN --");
            foreach (var serviceEndpoint in ListServicePoints())
            {
               
                PrintServicePointConnections(serviceEndpoint);
       
            }
            Output("-- END --");
        }

        private IEnumerable<ServicePoint> ListServicePoints()
        {
            var tableField = typeof(ServicePointManager).GetField("s_ServicePointTable", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            var table = (Hashtable)tableField.GetValue(null);
            var keys = table.Keys.Cast<object>().ToList();

            Output("ServicePoint count: {0}, DefaultConnectionLimit: {1}", keys.Count, ServicePointManager.DefaultConnectionLimit);
            foreach (var key in keys)
            {
                var val = ((WeakReference)table[key]);

                if (val == null)
                {
                    continue;
                }
                var target = val.Target;
                if (target == null)
                {
                    continue;
                }
                yield return target as ServicePoint;
            }

        }

        private void Output(string fmt, params object[] args)
        {
            _log(string.Format(fmt, args));
        }

        private void PrintServicePointConnections(ServicePoint sp)
        {
            var spType = sp.GetType();
            var privateOrPublicInstanceField = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            var connectionGroupField = spType.GetField("m_ConnectionGroupList", privateOrPublicInstanceField);
            var value = (Hashtable)connectionGroupField.GetValue(sp);

            //per user
            var connectionGroups = value.Keys.Cast<object>().ToList();
            var totalConnections = 0;

            if (!sp.ConnectionName.Contains("smtp"))
            {
                Output("ServicePoint: {0} (Connection Limit: {1}, Reported connections: {2})", sp.Address, sp.ConnectionLimit, sp.CurrentConnections);
            }
            else
            {
                Output("ServicePoint: {0} (Connection Limit: {1}, Reported connections: {2})", sp.ConnectionName, sp.ConnectionLimit, sp.CurrentConnections);
            }
            
            foreach (var key in connectionGroups)
            {
                var connectionGroup = value[key];
                var groupType = connectionGroup.GetType();
                var listField = groupType.GetField("m_ConnectionList", privateOrPublicInstanceField);
                var listValue = (ArrayList)listField.GetValue(connectionGroup);
                //Console.WriteLine("{3} {0}\nConnectionGroup: {1} Count: {2}",sp.Address, key,listValue.Count, DateTime.Now);
                Output("{0}", key);

                var c = 0;
                while (c < listValue.Count)
                {
                    var connection = listValue[c];
                    var connectionType = connection.GetType();
                    var socketField = connectionType.GetProperty("Socket", privateOrPublicInstanceField);
                    var canReadField = connectionType.GetProperty("CanRead", privateOrPublicInstanceField);
                    var canWriteField = connectionType.GetProperty("CanWrite", privateOrPublicInstanceField);
                    var canRead = (Boolean)canReadField.GetValue(connection);
                    var canWrite = (Boolean)canWriteField.GetValue(connection);
                    if (canRead || canWrite)
                    {
                        var socketValue = (Socket)socketField.GetValue(connection);
                        Output("Socket: {0} > {1} Read:{2} Write:{3}", socketValue.LocalEndPoint.ToString(), socketValue.RemoteEndPoint.ToString(), canRead.ToString(), canWrite.ToString());
                    }

                    c = c + 1;
                }
                totalConnections += listValue.Count;
            }

            Output("ConnectionGroupCount: {0}, Total Connections: {1}", connectionGroups.Count, totalConnections);
        }

        public static void Start(TimeSpan interval, TimeSpan? requestTimeout, int connectionLimitPerHost = 250, int connectionIdleTime = 100, int connectionLeaseTimeout = -1, Boolean useNagleAlgorithm = true, Boolean expect100Continue = true)
        {
            StartInner(new LogFactory().GetLogger("ServicePointMonitorLog").Trace, interval, requestTimeout, connectionLimitPerHost, connectionIdleTime, connectionLeaseTimeout, useNagleAlgorithm);
        }

        public static void StartConsole(TimeSpan interval, TimeSpan? requestTimeout, int connectionLimitPerHost = 20, int connectionIdleTime = 100, int connectionLeaseTimeout = -1, Boolean useNagleAlgorithm = true, Boolean expect100Continue = true)
        {
            StartInner(Console.WriteLine, interval, requestTimeout, connectionLimitPerHost, connectionIdleTime, connectionLeaseTimeout, useNagleAlgorithm);
        }

        private static void StartInner(Action<string> log, TimeSpan interval, TimeSpan? requestTimeout, int connectionLimitPerHost = 20, int connectionIdleTime = 100, int connectionLeaseTimeout = -1, Boolean useNagleAlgorithm = true, Boolean expect100Continue = true)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = connectionLimitPerHost;
            System.Net.ServicePointManager.UseNagleAlgorithm = useNagleAlgorithm;
            System.Net.ServicePointManager.Expect100Continue = expect100Continue;
            System.Net.ServicePointManager.MaxServicePointIdleTime = connectionIdleTime * 1000; //100 seconds - Close connection after this idle period
            if (connectionLeaseTimeout != -1)
            {
                DefaultConnectionLeaseTimeout = connectionLeaseTimeout * 1000;
            }
            else
            {
                DefaultConnectionLeaseTimeout = connectionLeaseTimeout;
            }

            DefaultRequestTimeout = requestTimeout ?? new TimeSpan(0, 1, 40);

            var thread = new Thread(() =>
            {
                while (true)
                {
                    var monitor = new ServicePointMonitor(log);
                    monitor.Print();

                    Thread.Sleep(interval);
                }
            })
            {
                IsBackground = true
            };
            thread.Start();
        }


    }
}
