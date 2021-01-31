using System;

namespace RedPoint.Chat.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string? message, object[] requestedId) : base(message)
        {
            RequestedIds = requestedId;
        }

        public EntityNotFoundException(string? message, Exception? innerException, object[] requestedId) : base(message, innerException)
        {
            RequestedIds = requestedId;
        }

        public object[] RequestedIds { get; }
    }
}