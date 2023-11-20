using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Couriers.Application.DTO;
using SwiftParcel.Services.Couriers.Application.Queries;
using SwiftParcel.Services.Couriers.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Couriers.Infrastructure.Mongo.Queries
{
    internal sealed class GetCourierHandler : IQueryHandler<GetCourier, CourierDto>
    {
        private readonly IMongoRepository<CourierDocument, Guid> _repository;

        public GetCourierHandler(IMongoRepository<CourierDocument, Guid> repository)
            => _repository = repository;
        
        public async Task<CourierDto> HandleAsync(GetCourier query, CancellationToken cancellationToken)
        {
            var document = await _repository.GetAsync(v => v.Id == query.CourierId);
            return document?.AsDto();
        }
    }
}