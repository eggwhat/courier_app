using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Identity.Core.Entities;
using SwiftParcel.Services.Identity.Core.Repositories;

namespace SwiftParcel.Services.Identity.Infrastructure.Persistence.Mongo.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User, Guid> _repository;

        public UserRepository(IMongoRepository<User, Guid> repository)
        {
            _repository = repository;
        }

        public Task<User> GetAsync(Guid id) => _repository.GetAsync(id);
        public Task<User> GetAsync(string email) => _repository.GetAsync(x => x.Email == email.ToLowerInvariant());
        public Task AddAsync(User user) => _repository.AddAsync(user);
        public Task UpdateAsync(User user) => _repository.UpdateAsync(user);
    }
}