using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using MongoDB.Driver;
using SwiftParcel.Services.Availability.Application.DTO;
using SwiftParcel.Services.Availability.Application.Qeries;
using SwiftParcel.Services.Availability.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Availability.Infrastructure.Mongo.Queries
{
    public class GetResourceHandler: IQueryHandler<GetResource, ResourceDto>
    {
        private readonly IMongoDatabase _database;

        public GetResourceHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<ResourceDto> HandleAsync(GetResource query, CancellationToken cancellationToken)
        {
            var document = await _database.GetCollection<ResourceDocument>("resources")
                .Find(r => r.Id == query.ResourceId)
                .SingleOrDefaultAsync();
            
            return document?.AsDto();
        }
    }
}