using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        public virtual string Code { get; }

        protected DomainException(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}
