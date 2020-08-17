using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RMMService.Models;
using RMMService.Services.TaskQueue;
using RMMService.Workers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RMMService.Services
{
    public class TaskShedulerService : IHostedService, IDisposable
    {
        private Timer timer;
        private readonly IServiceProvider services;
        private readonly Settings settings;
        private readonly ILogger logger;
        private readonly Random random = new Random();
        private readonly object syncRoot = new object();

        public TaskShedulerService(IServiceProvider services)
        {
            this.services = services;
            this.settings = services.GetRequiredService<Settings>();
            this.logger = services.GetRequiredService<ILogger<TaskShedulerService>>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var interval = settings?.RunInterval ?? 0;

            if (interval == 0)
            {
                logger.LogWarning("checkInterval is not defined in settings. Set to default: 60 sec.");
                interval = 60;
            }

            timer = new Timer(
                (e) => ProcessTask(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private void ProcessTask()
        {
            if (Monitor.TryEnter(syncRoot))
            {
                logger.LogInformation($"Process task started");

                DoWork();

                logger.LogInformation($"Process task finished");
                Monitor.Exit(syncRoot);
            }
            else
            {
                logger.LogInformation($"Processing is currently in progress. Skipped");
            }
        }

        private void DoWork()
        {
            var number = random.Next(20);

            var processor = services.GetRequiredService<TaskProcessor>();
            var queue = services.GetRequiredService<IBackgroundTaskQueue>();

            queue.QueueBackgroundWorkItem(token =>
            {
                return processor.RunAsync(number, token);
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
