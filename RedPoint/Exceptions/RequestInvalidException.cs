using System;

namespace RedPoint.Exceptions
{
    public class RequestInvalidException : Exception
    {
        public RequestInvalidException() { }

        public RequestInvalidException(string message) : base(message) { }

        public RequestInvalidException(string message, Exception inner) : base(message, inner) { }
    }
}