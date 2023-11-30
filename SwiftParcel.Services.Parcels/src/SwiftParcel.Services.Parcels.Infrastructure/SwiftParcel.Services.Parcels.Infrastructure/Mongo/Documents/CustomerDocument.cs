using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.Types;

namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents
{
    public class CustomerDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
    }
}
