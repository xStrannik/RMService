using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RMMService.Models;
using RMMService.Services;
using RMMService.Services.TaskQueue;
using RMMService.Workers;

namespace RMMService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(confBuilder =>
            {
                confBuilder.AddJsonFile("config.json");
                confBuilder.AddCommandLine(args);
            }).ConfigureLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
                services.AddHostedService<TaskShedulerService>();

                services.AddSingleton<Settings>();
                services.AddSingleton<TaskProcessor>();
                services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            }).UseWindowsService();
    }
}


/*
<Project Sdk = "Microsoft.NET.Sdk" >

  < PropertyGroup >
    < OutputType > Exe </ OutputType >
    < TargetFrameworks > netcoreapp3.1;net48</TargetFrameworks>
    <LangVersion>8.0</LangVersion>
    <ApplicationIcon>knigi.ico</ApplicationIcon>
    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
    <Version>0.0.1</Version>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include = "Microsoft.Extensions.Configuration.CommandLine" Version="3.1.6" />
    <PackageReference Include = "Microsoft.Extensions.Configuration.Json" Version="3.1.6" />
    <PackageReference Include = "Microsoft.Extensions.Hosting" Version="3.1.6" />
    <PackageReference Include = "Microsoft.Extensions.Logging" Version="3.1.6" />
    <PackageReference Include = "Microsoft.Extensions.Logging.Console" Version="3.1.6" />
    <PackageReference Include = "Microsoft.Extensions.Logging.Debug" Version="3.1.6" />
    <PackageReference Include = "Microsoft.NETFramework.ReferenceAssemblies" Version="1.0.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include = "Newtonsoft.Json" Version="12.0.3" />
  </ItemGroup>

  <ItemGroup Condition = "'$(TargetFramework)' == 'net48'" >
    < PackageReference Include="System.ServiceProcess.ServiceController" Version="4.7.0" />
    <Compile Remove = "Extensions\HostExtensions\HostExtensions.cs" />
    < None Include="Extensions\HostExtensions.cs" />
  </ItemGroup>

  <ItemGroup Condition = "'$(TargetFramework)' == 'netcoreapp3.1'" >
    < Compile Remove="WindowsService\**\*.cs" />
    <None Include = "WindowsService\**\*.cs" />
    < Compile Remove="Extensions\HostExtensions\WindowsHostExtensions.cs" />
    <None Include = "Extensions\HostExtensions\WindowsHostExtensions.cs" />
  </ ItemGroup >

  < ItemGroup >
    < Content Include="**\*.json" Exclude="bin\**\*;obj\**\*" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>

</Project>
*/