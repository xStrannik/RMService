using RMMService.Extensions.HostExtensions;
using RMMService.Models;
using RMMService.Models.Workers;
using RMMService.Services;
using RMMService.Services.TaskQueue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace RMMService
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //if ((args.Length > 1))
            //{
            //    foreach (var item in args)
            //    {
            //        Console.WriteLine(item.ToString());
            //    }
                
            //    //Process uinst = new Process();

            //    //uinst.StartInfo.FileName = Environment.GetFolderPath(SystemPath) + "\\msiexec";
            //    //uinst.StartInfo.Arguments = "/x " + args[1];
            //    //uinst.Start();
            //}
            //else
            //{
            //    // Normal app loading here
            //}

            var builder = new HostBuilder()
                .ConfigureAppConfiguration(confBuilder =>
                {
                    confBuilder.AddJsonFile("config.json");
                    confBuilder.AddCommandLine(args);
                })
                .ConfigureLogging((configLogging) =>
                {
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                })
                .ConfigureServices((services) =>
                {
                    services.AddHostedService<TaskShedulerService>();
                    services.AddHostedService<WorkerService>();

                    services.AddSingleton<Settings>();
                    services.AddSingleton<TaskProcessor>();
                    services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
                });

            await builder.RunService();

            //Console.WriteLine("What's your name?");

            //string name = Console.ReadLine();
            //Console.WriteLine($"Hello {name}!");
            //Console.WriteLine("Press any key to exit!");
            //Console.ReadLine();
        }
    }
}
