using System;

namespace RedPoint.Account.Exceptions
{
    public class LockOutException : Exception
    {
        public LockOutException()
        {
        }

        public LockOutException(string? message) : base(message)
        {
        }

        public LockOutException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}