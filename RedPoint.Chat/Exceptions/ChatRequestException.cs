using System;

namespace RedPoint.Chat.Exceptions
{
    public class ChatRequestException : Exception
    {
        public ChatRequestException()
        {
        }

        public ChatRequestException(string message) : base(message)
        {
        }

        public ChatRequestException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}