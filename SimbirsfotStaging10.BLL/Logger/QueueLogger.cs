using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SimbirsfotStaging10.DAL.Entities;

namespace SimbirsfotStaging10.Logger
{
    public class QueueLogger : IQueueLogger
    {
        readonly string _category;
        readonly Func<string, LogLevel, bool> _filter;


        public Queue<EventLog> QueueToSetLogs { get; set; }


        public QueueLogger(string category) => _category = category; 


        public QueueLogger(string category, Func<string, LogLevel, bool> filter) : this(category) => _filter = filter;


        public IDisposable BeginScope<TState>(TState state) => null;


        public bool IsEnabled(LogLevel logLevel) => _filter == null || _filter(_category, logLevel);


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                if(QueueToSetLogs!= null)
                    lock(QueueToSetLogs)
                    {
                        QueueToSetLogs.Enqueue(
                          new EventLog
                          {
                                Message = $"{logLevel.ToString()} - {eventId.Id} - {_category} - {formatter(state, exception)}",
                                Date = DateTime.Now,
                                EventType = eventId.Id
                          });
                    }        
            }
        }
    }
}
