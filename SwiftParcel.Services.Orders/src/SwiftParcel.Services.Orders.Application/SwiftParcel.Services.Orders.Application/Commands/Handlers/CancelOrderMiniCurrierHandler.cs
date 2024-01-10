using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Services.Clients;

namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class CancelOrderMiniCurrierHandler: ICommandHandler<CancelOrderMiniCurrier>
    {
        private readonly ILecturerApiServiceClient _lecturerApiServiceClient;
        public CancelOrderMiniCurrierHandler(ILecturerApiServiceClient lecturerApiServiceClient)
        {
            _lecturerApiServiceClient = lecturerApiServiceClient;
        }

        public async Task HandleAsync(CancelOrderMiniCurrier command, CancellationToken cancellationToken)
        {
            await _lecturerApiServiceClient.PostCancelOrder(command.OrderId.ToString());
        }
    }
}