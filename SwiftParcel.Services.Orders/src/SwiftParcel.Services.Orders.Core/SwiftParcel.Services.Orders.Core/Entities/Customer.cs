namespace SwiftParcel.Services.Orders.Core.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; private set; }

        public Customer(Guid id, string email, string fullName)
        {
            Id = id;
            Email = email;
            FullName = fullName;
        }
    }
}
