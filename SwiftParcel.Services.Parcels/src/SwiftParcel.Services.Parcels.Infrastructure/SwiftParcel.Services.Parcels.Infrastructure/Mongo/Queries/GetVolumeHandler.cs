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
using SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Queries
{
    internal sealed class GetVolumeHandler : IQueryHandler<GetVolume, VolumeDto>
    {
        private readonly IMongoRepository<ParcelDocument, Guid> _repository;

        public GetVolumeHandler(IMongoRepository<ParcelDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<VolumeDto> HandleAsync(GetVolume query, CancellationToken cancellationToken)
        {
            var documents = await _repository.Collection.FindAsync(FilterDefinition<ParcelDocument>.Empty);
            var documentList = await documents.ToListAsync();
            double volume = 0.0;

            foreach (var document in documentList)
            {
                volume += document.Width * document.Height * document.Depth;
            }

            return new VolumeDto { Volume = volume };
        }
    }
}
