using System;

namespace RedPoint.Chat.Exceptions.Security
{
    public class InvalidServerRequestException : Exception
    {
        public InvalidServerRequestException()
        {
        }

        public InvalidServerRequestException(string message) : base(message)
        {
        }

        public InvalidServerRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}