using System;

namespace RedPoint.Chat.Exceptions
{
    public class ChatRequestException : Exception
    {
        #nullable enable
        public ChatRequestException()
        {
        }

        public ChatRequestException(string? message) : base(message)
        {
        }

        public ChatRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}