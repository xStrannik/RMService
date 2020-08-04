using RMMService.WindowsService;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace RMMService.Extensions.HostExtensions
{
    public static class WindowsHostExtensions
    {
        public static async Task RunService(this IHostBuilder hostBuilder)
        {
            if (!Environment.UserInteractive)
            {
                await hostBuilder.RunAsServiceAsync();
            }
            else
            {
                await hostBuilder.RunConsoleAsync();
            }
        }
    }
}
