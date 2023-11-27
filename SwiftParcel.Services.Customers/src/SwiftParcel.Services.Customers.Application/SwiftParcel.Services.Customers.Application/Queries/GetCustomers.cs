using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using SwiftParcel.Services.Customers.Application.Dto;

namespace SwiftParcel.Services.Customers.Application.Queries
{
    public class GetCustomers: IQuery<IEnumerable<CustomerDto>>
    {
    }
}