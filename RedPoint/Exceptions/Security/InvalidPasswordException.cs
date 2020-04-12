using System;

namespace RedPoint.Exceptions.Security
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException()
        {
        }

        public InvalidPasswordException(string message) : base(message)
        {
        }

        public InvalidPasswordException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}