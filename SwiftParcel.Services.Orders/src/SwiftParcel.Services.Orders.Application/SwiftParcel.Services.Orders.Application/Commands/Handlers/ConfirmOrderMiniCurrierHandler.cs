using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Services.Clients;
using SwiftParcel.Services.Orders.Application.Exceptions;

namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class ConfirmOrderMiniCurrierHandler: ICommandHandler<ConfirmOrderMiniCurrier>
    {
        private readonly ILecturerApiServiceClient _lecturerApiServiceClient;
        public ConfirmOrderMiniCurrierHandler(ILecturerApiServiceClient lecturerApiServiceClient)
        {
            _lecturerApiServiceClient = lecturerApiServiceClient;
        }

        public async Task HandleAsync(ConfirmOrderMiniCurrier command, CancellationToken cancellationToken)
        {
            var response = await _lecturerApiServiceClient.PostConfirmOrder(command.OrderId.ToString());
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