using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Parcels.Application.DTO;
using SwiftParcel.Services.Parcels.Application.Queries;
using SwiftParcel.Services.Parcels.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Parcels.Infrastructure.Mongo.Queries
{
    internal sealed class GetParcelHandler : IQueryHandler<GetParcel, ParcelDto>
    {
        private readonly IMongoRepository<ParcelDocument, Guid> _repository;

        public GetParcelHandler(IMongoRepository<ParcelDocument, Guid> repository)
        {
            _repository = repository;
        }
        
        public async Task<ParcelDto> HandleAsync(GetParcel query, CancellationToken cancellationToken)
        {
            var document = await _repository.GetAsync(v => v.Id == query.ParcelId);
            return document?.AsDto();
        }
    }
}