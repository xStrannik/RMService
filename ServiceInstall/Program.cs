using System;
using System.Diagnostics;

namespace ServiceInstall
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create Service
            var processCreateService = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    //WorkingDirectory = @"C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48",
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    // Arguments = @"/k sc create ZRMMService binpath=C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48\RMMService.exe start=auto"
                    Arguments = @"/k sc create ZRMMService binpath=" + AppDomain.CurrentDomain.BaseDirectory + @"RMMService.exe start=auto",
                }
            };
            processCreateService.Start();

            //Start Srvice
            var processStartService = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    //WorkingDirectory = @"C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48",
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    Arguments = @"/k sc start ZRMMService"
                }
            };
            processStartService.Start();
        }
    }
}
