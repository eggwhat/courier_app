using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Application.Services.Clients;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class CreateOrderBaronomatHandler: ICommandHandler<CreateOrderBaronomat>
    {
        private readonly IBaronomatApiServiceClient _baronomatApiServiceClient;
        private readonly IParcelsServiceClient _parcelsServiceClient;

        public CreateOrderBaronomatHandler(IBaronomatApiServiceClient baronomatApiServiceClient, 
            IParcelsServiceClient parcelsServiceClient)
        {
            _baronomatApiServiceClient = baronomatApiServiceClient;
            _parcelsServiceClient = parcelsServiceClient;
        }
        public async Task HandleAsync(CreateOrderBaronomat command, CancellationToken cancellationToken)
        {
            var response = await _baronomatApiServiceClient.PostOfferAsync(command);
            if (response == null)
            {
                throw new BaronomatApiServiceConnectionException();
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new BaronomatApiServiceException(response.ReasonPhrase);
            }
        }
    }
}