using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Microsoft.Extensions.DependencyInjection;

namespace SwiftParcel.ExternalAPI.Lecturer.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
        {
            builder
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher();

            return builder;
        }
    }
}