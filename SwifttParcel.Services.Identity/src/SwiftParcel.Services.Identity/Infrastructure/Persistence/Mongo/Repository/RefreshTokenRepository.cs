using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Identity.Core.Domain;
using SwiftParcel.Services.Identity.Core.Repositories;

namespace SwiftParcel.Services.Identity.Infrastructure.Persistence.Mongo.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshToken, Guid> _repository;

        public RefreshTokenRepository(IMongoRepository<RefreshToken, Guid> repository)
        {
            _repository = repository;
        }

        public Task<RefreshToken> GetAsync(string token) => _repository.GetAsync(x => x.Token == token);
        public Task AddAsync(RefreshToken token) => _repository.AddAsync(token);
        public Task UpdateAsync(RefreshToken token) => _repository.UpdateAsync(token);
    }
}