using Microsoft.Extensions.Logging;
using SimbirsfotStaging10.DAL.Entities;
using System;
using System.Collections.Generic;


namespace SimbirsfotStaging10.Logger
{
    public static class QueueLoggerProviderExtensions
    {
        public static ILoggerFactory AddQueueLog(this ILoggerFactory factory, Func<string, LogLevel, 
            bool> filter = null, Queue<EventLog> queueToSetLogs = null )
        {
            factory?.AddProvider(new QueueLoggerProvider(filter) { QueueToSetLogs = queueToSetLogs});
            return factory;
        }
    }
}
