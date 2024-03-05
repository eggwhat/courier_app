using System.Windows.Input;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Application.Exceptions;

namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class ConfirmOrderHandler: ICommandHandler<ConfirmOrder>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public ConfirmOrderHandler(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task HandleAsync(ConfirmOrder command, CancellationToken cancellationToken)
        {
            switch (command.Company)
            {
                case Company.SwiftParcel:
                    await _commandDispatcher.SendAsync(new ConfirmOrderSwiftParcel(command));
                    break;
                case Company.MiniCurrier:
                    await _commandDispatcher.SendAsync(new ConfirmOrderMiniCurrier(command));
                    break;
                default:
                    throw new CompanyNotFoundException(command.Company.ToString());
            }
        }
    }
}