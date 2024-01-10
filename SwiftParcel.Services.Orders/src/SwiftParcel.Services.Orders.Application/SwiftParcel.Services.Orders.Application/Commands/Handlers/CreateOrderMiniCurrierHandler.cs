using Convey.CQRS.Commands;
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
            // TODO: Update
            await _lecturerApiServiceClient.PostOfferAsync(command);
        }
    }
}