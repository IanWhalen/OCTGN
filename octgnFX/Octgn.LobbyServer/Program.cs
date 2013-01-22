using System;
using System.Diagnostics;
using System.Reflection;

namespace Skylabs.LobbyServer
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Host;
    using Microsoft.Owin.Hosting;

    public static class Program
    {
        public static string Host { get; private set; }
        public static string SASLocation { get; private set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static List<IDisposable> disposables;
        private static void Main()
        {
            Log.InfoFormat("Start V{0}", Assembly.GetExecutingAssembly().GetName().Version);
#if(DEBUG)
            Host = ConfigurationManager.AppSettings["GameHostUrl-Debug"];
            SASLocation = ConfigurationManager.AppSettings["SASLocation-Debug"];
#else
            Host = ConfigurationManager.AppSettings["GameHostUrl-Release"];
            SASLocation = ConfigurationManager.AppSettings["SASLocation-Release"];
#endif
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainProcessExit;
            StartHosts();
        }

        private static void StartHosts()
        {
            Log.InfoFormat("Starting Host {0}",Host);

            using (WebApplication.Start<HubStart>(Host))
                Console.ReadLine();
        }

        private static void CurrentDomainProcessExit(object sender, EventArgs e)
        {
            Quit();
        }

        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Fatal("ERROR",e.ExceptionObject as Exception);
            Quit();
        }

        private static void Quit()
        {
            try
            {
                Gaming.Stop();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                if (Debugger.IsAttached) Debugger.Break();
            }
            foreach (var d in disposables)
            {
                try
                {
                    if (d != null)
                        d.Dispose();

                }
                catch
                {
                }
            }
            Trace.WriteLine("###PROCESS QUIT####");
        }
    }
}