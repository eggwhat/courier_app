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
using SwiftParcel.Services.Orders.Application.Commands;

namespace SwiftParcel.Services.Orders.Infrastructure.Brevo.Commands.Handlers
{
    public class SendCancellationEmailHandler: ICommandHandler<SendCancellationEmail>
    {
        private const string _senderName = "SwiftParcel";
        private const string _senderEmail = "switfparcel2023@gmail.com";
        private readonly TransactionalEmailsApi _apiInstance;
        private readonly ILogger<SendCancellationEmailHandler> _logger;
        public SendCancellationEmailHandler(ILogger<SendCancellationEmailHandler> logger)
        {
            _apiInstance = new TransactionalEmailsApi();
            _logger = logger;
        }

        public async System.Threading.Tasks.Task HandleAsync(SendCancellationEmail command, CancellationToken cancellationToken)
        {
            var sender = new SendSmtpEmailSender(_senderName, _senderEmail);
            var to = new List<SendSmtpEmailTo>
            {
                new SendSmtpEmailTo(command.CustomerEmail, command.CustomerName)
            };
            var parameters = new Dictionary<string, string>
            {
                { "orderId", command.OrderId.ToString()},
                { "reason", command.Reason}
            };

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(sender, to, null, null, null, null, null,
                                                      null, null, null, 2, parameters);
                CreateSmtpEmail result = await _apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                _logger.LogInformation("Email sent to {email}", command.CustomerEmail);
            }
            catch (Exception e)
            {
                throw new SmtpException(e.Message);
            }
        }
    }
}