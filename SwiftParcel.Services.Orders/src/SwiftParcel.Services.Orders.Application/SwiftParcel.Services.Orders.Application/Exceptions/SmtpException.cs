namespace SwiftParcel.Services.Orders.Application.Exceptions
{
    public class SmtpException : AppException
    {
        public override string Code { get; } = "smtp_exception";
        public SmtpException(string message) : base(message)
        {
        }
    }
}