using System;

namespace RedPoint.Account.Exceptions
{
    public class LockOutException : Exception
    {
        public LockOutException(string? message, string username) : base(message)
        {
            Username = username;
        }

        public LockOutException(string? message, Exception? innerException, string username) : base(message, innerException)
        {
            Username = username;
        }

        public string Username { get; }
    }
}