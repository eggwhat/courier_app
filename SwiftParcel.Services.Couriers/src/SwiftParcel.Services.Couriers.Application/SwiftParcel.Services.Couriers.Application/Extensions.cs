using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;

namespace SwiftParcel.Services.Couriers.Application
{
    public static class Extensions
    {
          public static IConveyBuilder AddApplication(this IConveyBuilder builder)
            => builder
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher();
    }
}