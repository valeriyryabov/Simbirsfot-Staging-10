using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Quartz;
using SimbirsfotStaging10.DAL.Data;
using SimbirsfotStaging10.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SimbirsfotStaging10.Logger
{
    public class DbLogSender : IJob
    {
        readonly ILogger logger;
        readonly Queue<EventLog> queueWithLogs;
        readonly string conString;

        public DbLogSender(ILoggerFactory loggerFactory, IScheduler scheduler)
        {
            logger = loggerFactory.CreateLogger(typeof(DbLogSender));
            logger.LogInformation("Job is created.");
            var jobDetail = scheduler.GetJobDetail(JobKey.Create("FromQueueIntoDb", "DbLogging")).Result;
            queueWithLogs = jobDetail.JobDataMap["queue"] as Queue<EventLog>;
            conString = jobDetail.JobDataMap["dbConnectionString"] as string;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await ExecuteJob(context);
        }

        async public Task ExecuteJob(IJobExecutionContext context)
        {
            try
            {
                if (queueWithLogs != null && queueWithLogs.Count != 0 && conString != null)
                {
                    var dbOptBuilder = new DbContextOptionsBuilder();
                    dbOptBuilder.UseSqlServer(conString);
                    using (var db = new SkiDBContext(dbOptBuilder.Options))
                    {
                        while (queueWithLogs.Count > 0)
                        {
                            EventLog log;
                            lock (queueWithLogs)
                            {
                                log = queueWithLogs.Dequeue();
                            }
                            await db.EventLogSet.AddAsync(log);
                            await db.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured on db logging.");
            
            }           
        }
    }
}
