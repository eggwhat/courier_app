using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Services.Clients;

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
            await _lecturerApiServiceClient.PostConfirmOrder(command.OrderId.ToString());
        }
    }
}