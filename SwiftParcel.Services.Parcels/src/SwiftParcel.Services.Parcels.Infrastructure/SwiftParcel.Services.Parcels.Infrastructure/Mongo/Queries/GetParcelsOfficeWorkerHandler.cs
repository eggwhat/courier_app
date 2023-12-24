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
    internal sealed class GetParcelsOfficeWorkerHandler : IQueryHandler<GetParcelsOfficeWorker, IEnumerable<ParcelDto>>
    {
        private readonly IMongoRepository<ParcelDocument, Guid> _repository;
        private readonly IAppContext _appContext;

        public GetParcelsOfficeWorkerHandler(IMongoRepository<ParcelDocument, Guid> repository, IAppContext appContext)
        {
            _repository = repository;
            _appContext = appContext;
        }

        public async Task<IEnumerable<ParcelDto>> HandleAsync(GetParcelsOfficeWorker query, CancellationToken cancellationToken)
        {
            var documents = _repository.Collection.AsQueryable();

            var identity = _appContext.Identity;
            if (!identity.IsOfficeWorker)
            {
                return Enumerable.Empty<ParcelDto>();
            }

            var orders = await documents.ToListAsync();
            return orders.Select(p => p.AsDto());
        }
    }
}
