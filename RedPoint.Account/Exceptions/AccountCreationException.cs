using System;
using System.Collections.Generic;

namespace RedPoint.Account.Exceptions
{
    #nullable enable
    public class AccountCreationException : Exception
    {
        public AccountCreationException(string? message) : base(message)
        {
        }
        
        public AccountCreationException(string? message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors;
        }

        public AccountCreationException(string? message, Exception? innerException, IEnumerable<string> errors) : base(message, innerException)
        {
            Errors = errors;
        }

        public IEnumerable<string>? Errors { get; }
    }
}