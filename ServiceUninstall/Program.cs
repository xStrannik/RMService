using System;
using System.Diagnostics;

namespace ServiceUninstall
{
    class Program
    {
        static void Main(string[] args)
        {
            string serviceName = "RMMService";

            //Console.WriteLine("Unistal...press any...111");
            //Console.ReadLine();

            //Stop Service
            var processStopService = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    //WorkingDirectory = @"C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48",
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    Arguments = @$"/k sc stop {serviceName}"
                }
            };
            processStopService.Start();

            //Delete
            var processDeleteService = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    //WorkingDirectory = @"C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48",
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    Arguments = @$"/k sc delete {serviceName}"
                }
            };
            processDeleteService.Start();
        }
    }
}
