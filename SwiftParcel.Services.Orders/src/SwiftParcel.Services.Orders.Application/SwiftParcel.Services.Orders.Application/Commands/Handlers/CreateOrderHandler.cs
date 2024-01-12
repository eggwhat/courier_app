using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using SwiftParcel.Services.Orders.Application.Services;
using SwiftParcel.Services.Orders.Core.Entities;
using SwiftParcel.Services.Orders.Core.Repositories;
using SwiftParcel.Services.Orders.Core.Exceptions;
using SwiftParcel.Services.Orders.Application.Services.Clients;
using SwiftParcel.Services.Orders.Application.Exceptions;


namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICommandDispatcher _commandDispatcher;

        public CreateOrderHandler(ICustomerRepository customerRepository, ICommandDispatcher commandDispatcher)
        {
            _customerRepository = customerRepository;
            _commandDispatcher = commandDispatcher;
        }
        public async Task HandleAsync(CreateOrder command, CancellationToken cancellationToken)
        {
            if (command.CustomerId != Guid.Empty && !await _customerRepository.ExistsAsync(command.CustomerId))
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }
            
            switch (command.Company)
            {
                case Company.SwiftParcel:
                    await _commandDispatcher.SendAsync(new CreateOrderSwiftParcel(command));
                    break;
                case Company.MiniCurrier:
                    await _commandDispatcher.SendAsync(new CreateOrderMiniCurrier(command));
                    break;
                default:
                    throw new CompanyNotFoundException(command.Company.ToString());
            }
        }
    }
}