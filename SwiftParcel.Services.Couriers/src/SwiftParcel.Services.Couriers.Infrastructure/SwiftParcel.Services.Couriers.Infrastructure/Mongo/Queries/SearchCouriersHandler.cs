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
    internal sealed class SearchCouriersHandler : IQueryHandler<SearchCouriers, PagedResult<CourierDto>>
    {
        private readonly IMongoRepository<CourierDocument, Guid> _repository;

        public SearchCouriersHandler(IMongoRepository<CourierDocument, Guid> repository)
            => _repository = repository;

        public async Task<PagedResult<CourierDto>> HandleAsync(SearchCouriers query, CancellationToken cancellationToken)
        {
            PagedResult<CourierDocument> pagedResult;
            if (query.PayloadCapacity <= 0 && query.LoadingCapacity <= 0 && query.Variants <= 0)
            {
                pagedResult = await _repository.BrowseAsync(_ => true, query);
            }
            else
            {
                pagedResult = await _repository.BrowseAsync(v => v.PayloadCapacity >= query.PayloadCapacity
                                                                 && v.LoadingCapacity >= query.LoadingCapacity &&
                                                                 v.Variants == query.Variants, query);
            }


            return pagedResult?.Map(d => d.AsDto());
        }
    }
}