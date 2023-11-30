using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Parcels.Application.Commands
{
    public class DeleteParcel : ICommand
    {
        public Guid ParcelId { get; }

        public DeleteParcel(Guid parcelId) => ParcelId = parcelId;
    }
}
