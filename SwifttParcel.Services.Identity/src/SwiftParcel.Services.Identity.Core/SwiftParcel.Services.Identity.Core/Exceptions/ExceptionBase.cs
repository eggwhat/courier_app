using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Identity.Core.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public abstract string Code { get; }

        public ExceptionBase(string message) : base(message)
        {
        }
    }
}