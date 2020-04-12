using System;

namespace RedPoint.Exceptions.Security
{
    public class InvalidChannelRequestException : Exception
    {
        public InvalidChannelRequestException()
        {
        }

        public InvalidChannelRequestException(string message) : base(message)
        {
        }

        public InvalidChannelRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}