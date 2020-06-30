﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Console.Extensions;
using Microsoft.Extensions.Logging;

namespace SampleWithSerilog
{
    public class SampleJob
    {
        private readonly ILogger<SampleJob> logger;
        private readonly IProgressBarFactory progressBarFactory;
        private readonly IJobManager jobManager;

        public SampleJob(ILogger<SampleJob> logger, IProgressBarFactory progressBarFactory, IJobManager jobManager)
        {
            this.logger = logger;
            this.progressBarFactory = progressBarFactory;
            this.jobManager = jobManager;
        }

        public async Task RunAsync()
        {
            logger.LogTrace("Test");
            logger.LogDebug("Test");
            logger.LogInformation("Test");
            logger.LogWarning("Test");
            logger.LogError("Test");
            logger.LogCritical("Test");

            var progress = progressBarFactory.Create("Test");
            for (var i = 0; i < 100; i++)
            {
                progress.SetValue(i + 1);
                await Task.Delay(100);
            }

            //Starting a job inside a job will mark it as a Continuation
            jobManager.Start<ContinuationJob>(x => x.RunAsync());
        }
    }
}
