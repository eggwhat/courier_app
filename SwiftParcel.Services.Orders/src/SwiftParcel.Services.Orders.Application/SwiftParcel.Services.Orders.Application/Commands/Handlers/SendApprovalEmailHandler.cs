using System.Windows.Input;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Configuration;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using SwiftParcel.Services.Orders.Application.Exceptions;
using SwiftParcel.Services.Orders.Core.Repositories;
using SwiftParcel.Services.Orders.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace SwiftParcel.Services.Orders.Application.Commands.Handlers
{
    public class SendApprovalEmailHandler: ICommandHandler<SendApprovalEmail>
    {
        private const string _senderName = "SwiftParcel";
        private const string _senderEmail = "switfparcel2023@gmail.com";
        private readonly TransactionalEmailsApi _apiInstance;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<SendApprovalEmailHandler> _logger;
        public SendApprovalEmailHandler(ICustomerRepository customerRepository, ILogger<SendApprovalEmailHandler> logger)
        {
            _apiInstance = new TransactionalEmailsApi();
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async System.Threading.Tasks.Task HandleAsync(SendApprovalEmail command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(command.CustomerId);
            if(customer is null)
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }

            var sender = new SendSmtpEmailSender(_senderName, _senderEmail);
            var to = new List<SendSmtpEmailTo>
            {
                new SendSmtpEmailTo(customer.Email, customer.FullName)
            };
            var parameters = new Dictionary<string, string>
            {
                { "orderId", command.OrderId.ToString()},
            };

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(sender, to, null, null, null, null, null,
                                                      null, null, null, 3, parameters);
                CreateSmtpEmail result = await _apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                _logger.LogInformation("Email sent to {email}", customer.Email);
            }
            catch (Exception e)
            {
                throw new SmtpException(e.Message);
            }
        }
    }
}