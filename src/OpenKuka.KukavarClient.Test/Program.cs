using Kukavar;
using OpenKuka.KukavarClient.Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenKuka.KukavarClient.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new KukavarClient(1, KukavarLogManager.GetLogger(1))
            //var client = new KukavarClient(1, null)
            {
                // ServerIP = IPAddress.Parse("192.168.10.4"),
                ServerIP = IPAddress.Parse("127.0.0.1"),
                ServerPort = 7000,
                MaxIdleTime = TimeSpan.FromMilliseconds(2000)
            };

            client.ConnectAsync().Wait();
            client.Run();
            client.SendAsync(KVPWriteQuery.Build(0, "$Test", "4"), Callback);
            client.SendAsync(KVPWriteQuery.Build(0, "$Tester", "42"), Callback);
            var testesg = client.SendAsync(KVReadQuery.Build(0, "$XRODRACKPOS1")).Result;
            Trace.WriteLine(testesg);
            // var test = client.SendAsync(client.PingMsg, 0, 1);
            // test.Start();
            // test.Wait();
            // Task.Run(() => {
            //     Task.Delay(1000);
            //     var chrono = Stopwatch.StartNew();
            //     for (int i = 0; i < 50; i++)
            //     {
            //         var res = client.SendAsync(KVReadQuery.Build(0, "$OV_PRO")).Result;
            //         Trace.WriteLine(res);
            //     }
            //     chrono.Stop();
            //     Trace.WriteLine("chrono = " + chrono.ElapsedMilliseconds);
            //
            //
            // });

            //client.Connecting += ConnectingHandler;
            //client.Connected += Connected;
            //client.ConnectionError += ConnectionErrorHandler;
            //client.Closing += ClosingErrorHandler;
            //client.Closed += ClosedErrorHandler;

            Console.ReadKey();
        }

        private static Task Callback(KVReply reply)
        {
            Trace.WriteLine(reply.Answer);
            return Task.CompletedTask;
            // throw new NotImplementedException();
        }
    }
}
