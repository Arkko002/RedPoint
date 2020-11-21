using System;

namespace RedPoint.Account.Exceptions
{
    public class AuthorizationAttemptException : Exception
    {
        public AuthorizationAttemptException()
        {
        }

        public AuthorizationAttemptException(string? message) : base(message)
        {
        }

        public AuthorizationAttemptException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}