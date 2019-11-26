using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SimbirsfotStaging10.DAL.Entities;

namespace SimbirsfotStaging10.Logger
{
    public interface IQueueLogger:ILogger
    {
        Queue<EventLog> QueueToSetLogs { get; set; }
    }
}
