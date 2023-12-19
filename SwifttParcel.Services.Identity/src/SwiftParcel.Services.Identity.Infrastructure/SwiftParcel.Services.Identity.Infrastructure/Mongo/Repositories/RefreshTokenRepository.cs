using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Identity.Core.Entities;
using SwiftParcel.Services.Identity.Core.Repositories;
using SwiftParcel.Services.Identity.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Identity.Infrastructure.Mongo.Repositories
{
    internal sealed class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshTokenDocument, Guid> _repository;

        public RefreshTokenRepository(IMongoRepository<RefreshTokenDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<RefreshToken> GetAsync(string token)
        {
            var refreshToken = await _repository.GetAsync(x => x.Token == token);

            return refreshToken?.AsEntity();
        }

        public Task AddAsync(RefreshToken token) => _repository.AddAsync(token.AsDocument());

        public Task UpdateAsync(RefreshToken token) => _repository.UpdateAsync(token.AsDocument());
    }
}