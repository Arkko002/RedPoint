using System;

namespace RedPoint.Exceptions
{
    public class ApplicationUserNotFoundException : Exception
    {
        public ApplicationUserNotFoundException() { }

        public ApplicationUserNotFoundException(string message) : base(message) { }

        public ApplicationUserNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}