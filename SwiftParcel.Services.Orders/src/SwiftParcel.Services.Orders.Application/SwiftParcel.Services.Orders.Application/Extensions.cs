<<<<<<< HEAD
﻿using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
>>>>>>> bb4eadacae4c3d0c9391d1ebc141e92ddf13e7ff

namespace SwiftParcel.Services.Orders.Application
{
    public static class Extensions
    {
<<<<<<< HEAD
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
            => builder
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher();
=======
        
>>>>>>> bb4eadacae4c3d0c9391d1ebc141e92ddf13e7ff
    }
}