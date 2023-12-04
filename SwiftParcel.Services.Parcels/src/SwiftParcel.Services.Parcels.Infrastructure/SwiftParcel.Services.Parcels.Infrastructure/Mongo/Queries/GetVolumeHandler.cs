using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using SwiftParcel.Services.Parcels.Application.DTO;
using SwiftParcel.Services.Parcels.Application.Queries;
using SwiftParcel.Services.Parcels.Core.Services;
using SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Queries
{
    internal sealed class GetVolumeHandler : IQueryHandler<GetVolume, VolumeDto>
    {
        private readonly IMongoRepository<ParcelDocument, Guid> _repository;
        private readonly IParcelService _parcelService;

        public GetVolumeHandler(IMongoRepository<ParcelDocument, Guid> repository, IParcelService parcelService)
        {
            _repository = repository;
            _parcelService = parcelService;
        }

        public async Task<VolumeDto> HandleAsync(GetVolume query, CancellationToken cancellationToken)
        {
            var documents = await _repository.Collection.FindAsync(FilterDefinition<ParcelDocument>.Empty);
            var parcels = documents.ToList().Select(d => d.AsEntity());

            double volume = _parcelService.CalculateVolume(parcels);
            return new VolumeDto { Volume = volume };
        }
    }
}
