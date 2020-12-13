using System;

namespace RedPoint.Chat.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string? message, int requestedId) : base(message)
        {
            RequestedId = requestedId;
        }

        public EntityNotFoundException(string? message, Exception? innerException, int requestedId) : base(message, innerException)
        {
            RequestedId = requestedId;
        }

        public int RequestedId { get; }
    }
}