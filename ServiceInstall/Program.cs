using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace ServiceInstall
{
    class Program
    {
        static string _serviceName = "RMMService";

        static void Main(string[] args)
        {

            //Console.WriteLine("Before...1234 press any...111");
            //Console.ReadLine();

            ServiceController[] sc = ServiceController.GetServices();

            var checkService = ServiceController.GetServices().Any(s => s.ServiceName.Equals(_serviceName));

            ServiceController service = new ServiceController(_serviceName);
            if (checkService && (service.Status == ServiceControllerStatus.Running || service.Status == ServiceControllerStatus.Stopped))
            {
                //Console.WriteLine("Service active press any...");
                //Console.ReadLine();

                Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"ServiceUninstall.exe");

                System.Threading.Thread.Sleep(1000);

                StartService();

                //service.Start();
                //service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMinutes(1));
            }
            else
            {
                //Console.WriteLine("CANT FIND");
                //Console.ReadLine();

                StartService();
            }
        }

        private static void StartService()
        {
            //Console.WriteLine("Start servise press any...");
            //Console.ReadLine();

            //Create Service
            var processCreateService = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    //WorkingDirectory = @"C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48",  sc create ZRMMService binpath=B:\1111\RMMService.exe start = auto
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    // Arguments = @"/k sc create ZRMMService binpath=C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48\RMMService.exe start=auto"
                    Arguments = $"/k sc create {_serviceName} binpath=\"" + AppDomain.CurrentDomain.BaseDirectory + "RMMService.exe\" start=auto",
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
                    Arguments = @$"/k sc start {_serviceName}"
                }
            };
            processStartService.Start();
        }


    }
}
