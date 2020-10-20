using System;

namespace RedPoint.Account.Exceptions
{
    public class AccountRequestException : Exception
    {
        public AccountRequestException()
        {
        }

        public AccountRequestException(string message) : base(message)
        {
        }

        public AccountRequestException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}