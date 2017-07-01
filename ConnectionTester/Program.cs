using Solution.Base.App_Start;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionTester
{
    class Program
    {
        static int recieved = 0;
        static void Main(string[] args)
        {

            //ThreadPool.SetMinThreads(100, 50);
            ServicePointMonitor.StartConsole(new TimeSpan(0, 0, 1), new TimeSpan(0, 20, 0), 250, 100,  -1, false, false);

            int i = 10000;
            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 10, 0);
            var url = "http://partners.api.skyscanner.net/apiservices/browseroutes/v1.0/GB/GBP/en-GB/LON/UK/anytime/anytime?apiKey=fl125488904091534567725289422462";

            List<Task> tasks = new List<Task>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (i > 0)
            {
                //RequestAsync(client, "http://aspnetmonsters.com").Wait();
                //tasks.Add(RequestAsync(client, "https://aspnetmonsters.com"));
                tasks.Add(RequestAsync(client, url));

                i = i - 1;
            }
            Task.WhenAll(tasks).ContinueWith(t => Console.WriteLine("Completed:" + stopwatch.Elapsed.TotalSeconds.ToString()));

            Console.ReadLine();
        }

        private static async Task RequestAsync(HttpClient client, string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Version = HttpVersion.Version11;
            //request.Headers.ConnectionClose = false;
           //request.Headers.Add("Connection", "Close");
           //request.Headers.Add("Connection", "keep-alive");
           //request.Headers.Add("Keep-Alive", "600");

           Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (HttpResponseMessage response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                byte[] result = await response.Content.ReadAsByteArrayAsync();
                string result2 = await response.Content.ReadAsStringAsync();
                recieved = recieved + 1; 
                Console.WriteLine(recieved + ". Response received - " + stopwatch.Elapsed.TotalSeconds.ToString());

            }
        }

    }
}
