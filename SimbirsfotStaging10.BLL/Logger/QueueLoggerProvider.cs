using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using SimbirsfotStaging10.DAL.Entities;

namespace SimbirsfotStaging10.Logger
{
    public class QueueLoggerProvider : ILoggerProvider
    {
        readonly Func<string, LogLevel, bool> _filter;

        public Queue<EventLog> QueueToSetLogs { get; set; }


        public QueueLoggerProvider(Func<string, LogLevel, bool> filter)
        {
            _filter = filter;
        }


        public ILogger CreateLogger(string categoryName)
        {
            return new QueueLogger(categoryName, _filter) { QueueToSetLogs = QueueToSetLogs};
        }


        public void Dispose() { }
    }
}
