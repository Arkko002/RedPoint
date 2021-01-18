using System;

namespace RedPoint.Account.Exceptions
{
    public class AccountLoginException : Exception
    {
        public AccountLoginException(string? message, string username) : base(message)
        {
            Username = username;
        }

        public AccountLoginException(string? message, Exception? innerException, string username) : base(message, innerException)
        {
            Username = username;
        }

        public string Username { get; }
    }
}