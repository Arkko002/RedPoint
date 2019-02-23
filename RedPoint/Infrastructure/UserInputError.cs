using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedPoint.Infrastructure
{
    public enum UserInputError
    {
        InputValid,
        NoChannel,
        NoServer,
        UserNotInServer,
        CantWrite,
        CantView
    } 
}
