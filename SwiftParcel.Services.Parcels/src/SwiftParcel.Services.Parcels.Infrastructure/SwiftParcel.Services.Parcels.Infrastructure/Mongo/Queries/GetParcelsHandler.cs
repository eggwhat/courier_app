using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SwiftParcel.Services.Parcels.Application;
using SwiftParcel.Services.Parcels.Application.DTO;
using SwiftParcel.Services.Parcels.Application.Queries;
using SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Queries
{
    internal sealed class GetParcelsHandler : IQueryHandler<GetParcels, IEnumerable<ParcelDto>>
    {
        private readonly IMongoRepository<ParcelDocument, Guid> _repository;
        private readonly IAppContext _appContext;

        public GetParcelsHandler(IMongoRepository<ParcelDocument, Guid> repository, IAppContext appContext)
        {
            _repository = repository;
            _appContext = appContext;
        }

        public async Task<IEnumerable<ParcelDto>> HandleAsync(GetParcels query, CancellationToken cancellationToken)
        {
            var documents = _repository.Collection.AsQueryable();

            if (query.CustomerId.HasValue)
            {
                var identity = _appContext.Identity;
                if (identity.IsAuthenticated && identity.Id != query.CustomerId && !identity.IsAdmin)
                {
                    return Enumerable.Empty<ParcelDto>();
                }

                documents = documents.Where(p => p.CustomerId == query.CustomerId);
            }

            var orders = await documents.ToListAsync();
            return orders.Select(p => p.AsDto());
        }
    }
}
