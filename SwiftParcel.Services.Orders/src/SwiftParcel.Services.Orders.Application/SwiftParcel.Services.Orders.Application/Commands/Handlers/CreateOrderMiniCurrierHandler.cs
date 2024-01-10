using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Application.Services.Clients;

namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class CreateOrderMiniCurrierHandler: ICommandHandler<CreateOrderMiniCurrier>
    {
        private readonly ILecturerApiServiceClient _lecturerApiServiceClient;

        public CreateOrderMiniCurrierHandler(ILecturerApiServiceClient lecturerApiServiceClient)
        {
            _lecturerApiServiceClient = lecturerApiServiceClient;
        }
        public async Task HandleAsync(CreateOrderMiniCurrier command, CancellationToken cancellationToken)
        {
            // TODO: Add validation
            var response = await _lecturerApiServiceClient.PostOfferAsync(command);
            if (response == null)
            {
                throw new LecturerApiServiceConnectionException();
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new LecturerApiServiceException(response.ReasonPhrase);
            }
        }
    }
}