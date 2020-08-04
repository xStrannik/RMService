using System;
using System.IO;
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

namespace RMMServiceInstallerWin
{
    class Program
    {
        static void Main(string[] args)
        {
            string serviceName = "ZRMMService";

            var currentDirectory = Environment.CurrentDirectory;

            //var sad = new System.Configuration.Install.Installer();

            //FileInfo fileInfo = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

            //var asaa = new ServiceInstaller();


            //Console.WriteLine(fileInfo.DirectoryName);
            //Console.WriteLine("Hello World!");
            //Console.WriteLine("Hello World!");

            //Console.WriteLine(currentDirectory);

            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);

            //var path = fileInfo.DirectoryName;


            //if (File.Exists(path))
            //{
            //    // This path is a file
            //    ProcessFile(path);
            //}
            //else if (Directory.Exists(path))
            //{
            //    // This path is a directory
            //    ProcessDirectory(path);
            //}
            //else
            //{
            //    Console.WriteLine("{0} is not a valid file or directory.", path);
            //}

            //var qwe = new ServiceInstaller();
            //qwe.ServiceInstaller_Committed();



            //System.Diagnostics.Process process = new System.Diagnostics.Process();
            //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

            //startInfo.FileName = "cmd.exe";
            //startInfo.WorkingDirectory = @"C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48";
            ////startInfo.Arguments = @"/k sc create ZRMMService binpath=C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48\RMMService.exe";
            ////startInfo.Arguments = @"/k sc start ZRMMService";
            ////startInfo.Arguments = @"/k sc stop ZRMMService";
            ////startInfo.Arguments = @"/k sc delete ZRMMService";
            //startInfo.ArgumentList.Add(@"/k sc create ZRMMService binpath=C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48\RMMService.exe start=auto");
            ////startInfo.ArgumentList.Add(@"/k sc start ZRMMService");



            //process.StartInfo = startInfo;
            //process.Start();  && (args[0] == "-uninstall")


            if ((args.Length > 1) )
            {
                foreach (var item in args)
                {
                    Console.WriteLine(item);
                }
            }

            ServiceController service = new ServiceController(serviceName);
            if (service.Status != ServiceControllerStatus.Running)
            {
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMinutes(1));
            }

            //Console.WriteLine(@"/k sc create ZRMMService binpath=" + AppDomain.CurrentDomain.BaseDirectory + @"RMMService.exe start=auto");


            ////Create Service
            //var processCreateService = new System.Diagnostics.Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        WindowStyle = ProcessWindowStyle.Hidden,
            //        CreateNoWindow = true,
            //        FileName = "cmd.exe",
            //        //WorkingDirectory = @"C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48",
            //        WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
            //        // Arguments = @"/k sc create ZRMMService binpath=C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48\RMMService.exe start=auto"
            //        Arguments = @"/k sc create ZRMMService binpath=" + AppDomain.CurrentDomain.BaseDirectory + " start=auto",
            //    }
            //};
            //processCreateService.Start();

            ////Start Srvice
            //var processStartService = new System.Diagnostics.Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        WindowStyle = ProcessWindowStyle.Hidden,
            //        CreateNoWindow = true,
            //        FileName = "cmd.exe",
            //        //WorkingDirectory = @"C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48",
            //        WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
            //        Arguments = @"/k sc start ZRMMService"
            //    }
            //};
            //processStartService.Start();





            ////Stop Service
            //var processStopService = new System.Diagnostics.Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        WindowStyle = ProcessWindowStyle.Hidden,
            //        CreateNoWindow = true,
            //        FileName = "cmd.exe",
            //        WorkingDirectory = @"C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48",
            //        Arguments = @"/k sc stop ZRMMService"
            //    }
            //};
            //processStopService.Start();

            ////Delete
            //var processDeleteService = new System.Diagnostics.Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        WindowStyle = ProcessWindowStyle.Hidden,
            //        CreateNoWindow = true,
            //        FileName = "cmd.exe",
            //        WorkingDirectory = @"C:\Users\Stranik\source\repos\RMMService\RMMService\bin\Debug\net48",
            //        Arguments = @"/k sc delete ZRMMService"
            //    }
            //};
            //processDeleteService.Start();

            Console.ReadLine();

            //Process uinst = new Process();

            //var asd111 = (Environment.CurrentDirectory);

            //var asd = (uinst.MachineName);


            //Console.WriteLine(asd111);


            //uinst.StartInfo.FileName = Environment.GetFolderPath(SystemPath) + "\\msiexec";
            //uinst.StartInfo.Arguments = "/x " + args[1];
            //uinst.Start();
        }

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);
        }

        [RunInstaller(true)]
        public class ServiceInstaller : Installer
        {
            string strServiceName = "MyServiceName";

            public ServiceInstaller()
            {
                var processInstaller = new ServiceProcessInstaller();
                System.ServiceProcess.ServiceInstaller serviceInstaller = new System.ServiceProcess.ServiceInstaller();

                processInstaller.Account = ServiceAccount.LocalSystem;
                processInstaller.Username = null;
                processInstaller.Password = null;

                serviceInstaller.DisplayName = strServiceName;
                serviceInstaller.StartType = ServiceStartMode.Automatic;

                serviceInstaller.ServiceName = strServiceName;

                this.Installers.Add(processInstaller);
                this.Installers.Add(serviceInstaller);

                //this.Committed += new InstallEventHandler(ServiceInstaller_Committed);
            }

            public void ServiceInstaller_Committed()
            {
                // Auto Start the Service Once Installation is Finished.
                var controller = new ServiceController(strServiceName);
                controller.Start();

            }

            protected override void OnAfterInstall(IDictionary savedState)
            {
                base.OnAfterInstall(savedState);

            }
        }
    }
}
