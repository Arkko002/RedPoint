using System;

namespace RedPoint.Exceptions.Security
{
    public class LackOfPermissionException : Exception
    {
        public LackOfPermissionException()
        {
        }

        public LackOfPermissionException(string message) : base(message)
        {
        }

        public LackOfPermissionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}