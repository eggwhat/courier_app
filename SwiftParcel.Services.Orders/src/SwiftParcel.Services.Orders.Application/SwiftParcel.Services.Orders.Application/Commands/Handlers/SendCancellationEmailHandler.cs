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
    public class SendCancellationEmailHandler: ICommandHandler<SendCancellationEmail>
    {
        private const string _senderName = "SwiftParcel";
        private const string _senderEmail = "switfparcel2023@gmail.com";
        private readonly TransactionalEmailsApi _apiInstance;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<SendCancellationEmailHandler> _logger;
        public SendCancellationEmailHandler(ICustomerRepository customerRepository, ILogger<SendCancellationEmailHandler> logger)
        {
            _apiInstance = new TransactionalEmailsApi();
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async System.Threading.Tasks.Task HandleAsync(SendCancellationEmail command, CancellationToken cancellationToken)
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
            var TextContent = command.Body;
            string HtmlContent = null;

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(sender, to, null, null, HtmlContent, TextContent, command.Subject);
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