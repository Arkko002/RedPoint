using System;

namespace RedPoint.Account.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string? message, string username) : base(message)
        {
            Username = username;
        }

        public InvalidPasswordException(string? message, Exception? innerException, string username) : base(message, innerException)
        {
            Username = username;
        }

        public string Username { get; }
    }
}