﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RMMService.Models;
using RMMService.Services.TaskQueue;
using RMMService.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RMMService.Services
{
    public class WorkerService : BackgroundService
    {
        private readonly IBackgroundTaskQueue taskQueue;
        private readonly ILogger<TaskProcessor> logger;
        private readonly Settings settings;

        public WorkerService(IBackgroundTaskQueue taskQueue, ILogger<TaskProcessor> logger, Settings settings)
        {
            this.taskQueue = taskQueue;
            this.logger = logger;
            this.settings = settings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var workersCount = settings.WorkersCount;
            var workers = Enumerable.Range(0, workersCount).Select(num => RunInstance(num, stoppingToken));

            await Task.WhenAll(workers);
        }

        private async Task RunInstance(int num, CancellationToken token)
        {
            logger.LogInformation($"#{num} is starting");

            while (!token.IsCancellationRequested)
            {
                var workItem = await taskQueue.DequeueAsync(token);

                try
                {
                    logger.LogInformation($"#{num}: Processing task. Queqe size: {taskQueue.Size}.");
                    await workItem(token);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"#{num}: Error occurred executing task.");
                }
            }

            logger.LogInformation($"#{num} is stopping");
        }
    }
}
