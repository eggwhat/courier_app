using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
