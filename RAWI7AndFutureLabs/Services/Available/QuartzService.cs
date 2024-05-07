using System;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quartz;
using Quartz.Impl;

namespace RAWI7AndFutureLabs.Services.Available
{

    public class QuartzScheduledTaskService : BackgroundService
    {
        private readonly IScheduler _scheduler;

        public QuartzScheduledTaskService()
        {
            _scheduler = new StdSchedulerFactory().GetScheduler().GetAwaiter().GetResult();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _scheduler.Start(stoppingToken);

            var job = JobBuilder.Create<SampleJob>()
                .WithIdentity("sampleJob", "group1")
                .Build();

            var trigger = Quartz.TriggerBuilder.Create()
                .WithIdentity("sampleTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(60)
                    .RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(job, trigger, stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Shutdown(cancellationToken);
        }
    }

    public class SampleJob : IJob
    {
        private readonly ILogger<SampleJob> _logger;

        public SampleJob(ILogger<SampleJob> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Executings interesting Quartz.NET task which scheduled");

            await WriteToLogFile("Executings interesting Quartz.NET task which scheduled");
        }

        private async Task WriteToLogFile(string message)
        {
            string logFilePath = "quartz_task.log";
            try
            {
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    await writer.WriteLineAsync($"{DateTime.UtcNow}: {message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error writing to log file: {ex.Message}");
            }
        }
    }
}

