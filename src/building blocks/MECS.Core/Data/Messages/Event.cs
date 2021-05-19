using MediatR;
using System;

namespace MECS.Core.Data.Messages
{
    public class Event : Message, INotification
    {
        public Event()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
    }
}
