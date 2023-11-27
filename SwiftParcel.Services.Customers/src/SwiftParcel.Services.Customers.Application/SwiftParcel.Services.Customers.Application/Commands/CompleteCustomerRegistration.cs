using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftParcel.Services.Customers.Application.Commands
{
    public class CompleteCustomerRegistration : ICommand
    {
        public Guid CustomerId { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }  
        public string FullName => $"{FirstName} {LastName}";
        public string Address { get; }

        public CompleteCustomerRegistration(Guid customerId, string firstName, string lastName, string address)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }
    }
}