using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class CancelOrderHandler: ICommandHandler<CancelOrder>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public CancelOrderHandler(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task HandleAsync(CancelOrder command, CancellationToken cancellationToken)
        {
             switch (command.Company)
            {
                case Company.SwiftParcel:
                    await _commandDispatcher.SendAsync(new CancelOrderSwiftParcel(command));
                    break;
                case Company.MiniCurrier:
                    await _commandDispatcher.SendAsync(new CancelOrderMiniCurrier(command));
                    break;
                default:
                    throw new CompanyNotFoundException(command.Company.ToString());
            }
        }
    }
}