using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace SwiftParcel.Services.Customers.Application.Commands
{
    public class CompleteCustomerRegistration : ICommand
    {
        public Guid CustomerId { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }  
        public string FullName => $"{FirstName} {LastName}";
        public string Address { get; }
        public string SourceAddress { get; private set; }

        public CompleteCustomerRegistration(Guid customerId, string firstName, string lastName, string address, string sourceAddress)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            SourceAddress = sourceAddress;
        }
    }
}