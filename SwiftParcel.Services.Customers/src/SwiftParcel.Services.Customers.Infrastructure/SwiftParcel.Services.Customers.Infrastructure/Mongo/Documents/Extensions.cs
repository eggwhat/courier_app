using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftParcel.Services.Customers.Application.Dto;
using SwiftParcel.Services.Customers.Core.Entities;

namespace SwiftParcel.Services.Customers.Infrastructure.Mongo.Documents
{
    public static class Extensions
    {
        public static Customer AsEntity(this CustomerDocument document)
            => new Customer(document.Id, document.Email, document.CreatedAt, document.FirstName, document.LastName, document.Address,
                    document.SourceAddress, document.IsVip, document.State, document.CompletedOrders);

        public static CustomerDocument AsDocument(this Customer entity)
            => new CustomerDocument
            {
                Id = entity.Id,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Address = entity.Address,
                IsVip = entity.IsVip,
                State = entity.State,
                CreatedAt = entity.CreatedAt,
                CompletedOrders = entity.CompletedOrders
            };

        public static CustomerDto AsDto(this CustomerDocument document)
            => new CustomerDto
            {
                Id = document.Id,
                State = document.State.ToString().ToLowerInvariant(),
                CreatedAt = document.CreatedAt,
            };

        public static CustomerDetailsDto AsDetailsDto(this CustomerDocument document)
            => new CustomerDetailsDto
            {
                Id = document.Id,
                Email = document.Email,
                FirstName = document.FirstName,
                LastName = document.LastName,
                Address = document.Address,
                IsVip = document.IsVip,
                State = document.State.ToString().ToLowerInvariant(),
                CreatedAt = document.CreatedAt,
                CompletedOrders = document.CompletedOrders
            };
    }
}