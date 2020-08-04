using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMService.Extensions.HostExtensions
{
    public static class HostExtensions
    {
        public static Task RunService(this IHostBuilder hostBuilder)
        {
            return hostBuilder.RunConsoleAsync();
        }
    }
}
