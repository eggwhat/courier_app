namespace SwiftParcel.Services.Orders.Application.Exceptions
{
    public class CompanyNotFoundException: AppException
    {
        public override string Code { get; } = "company_not_found";
        public string CompanyName { get; }

        public CompanyNotFoundException(string companyName) : base($"Company with name {companyName} was not found.")
        {
            CompanyName = companyName;
        }
    }
}