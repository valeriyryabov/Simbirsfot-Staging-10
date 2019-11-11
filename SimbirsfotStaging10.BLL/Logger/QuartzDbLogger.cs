using Microsoft.AspNetCore.Builder;
using SimbirsfotStaging10.DAL.Entities;
using Quartz;
using System.Collections.Generic;
using Quartz.Impl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Quartz.Spi;

namespace SimbirsfotStaging10.Logger
{
    public static class QuartzDbLogger
    {

        public static async void ScheduleDbLoggingByTimer(IScheduler scheduler,string conString, Queue<EventLog> queueWithLogs)
        {
            var jobDetail = JobBuilder.Create<DbLogSender>()
                .WithIdentity("FromQueueIntoDb", "DbLogging")
                .Build();
            jobDetail.JobDataMap.Add("queue", queueWithLogs);
            jobDetail.JobDataMap.Add("dbConnectionString", conString);
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("FromQueueIntoDb", "DbLogging")
                .StartNow()
                .WithSimpleSchedule((sch) =>
                {
                    sch.WithIntervalInSeconds(10);
                    sch.RepeatForever();
                })
                .Build();
            await scheduler.ScheduleJob(jobDetail, trigger);
        }
        

        public static void AddQuartzDbLogging(this IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, QuartzJobFactory>();
            services.AddSingleton<DbLogSender>();
            services.AddSingleton((provider) =>
            {
                var scheduler = new StdSchedulerFactory().GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                return scheduler;
            });
        }


        public static void StartJob(this IApplicationBuilder app, IApplicationLifetime lifetime, string conString,
            Queue<EventLog> queueWithLogs)
        {
            var scheduler = app.ApplicationServices.GetService<IScheduler>();
            ScheduleDbLoggingByTimer(scheduler, conString, queueWithLogs);
            lifetime.ApplicationStarted.Register(() => scheduler.Start());
            lifetime.ApplicationStopped.Register(() => scheduler.Shutdown());
        }
    }
}
