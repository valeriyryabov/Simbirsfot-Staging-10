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

        public DbLogSender(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger(typeof(DbLogSender));
            logger.LogInformation("Job is created.");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await ExecuteJob(context);
        }

        async public Task ExecuteJob(IJobExecutionContext context)
        {
            try
            {
                var queueWithLogs = context.JobDetail.JobDataMap["queue"] as Queue<EventLog>;
                var conString = context.JobDetail.JobDataMap["dbConnectionString"] as string;
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
