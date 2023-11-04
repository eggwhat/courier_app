using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using SwiftParcel.Services.Identity.Application.Query;
using SwiftParcel.Services.Identity.Identity.Application.UserDTO;
using SwiftParcel.Services.Identity.Infrastructure.Mongo.Documents;

namespace SwiftParcel.Services.Identity.Infrastructure.Mongo.Query
{
    internal sealed  class GetUserHandler: IQueryHandler<GetUser, UserDto>
    {
        private readonly IMongoRepository<UserDocument, Guid> _userRepository;

        public GetUserHandler(IMongoRepository<UserDocument, Guid> userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<UserDto> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetAsync(query.UserId);

            return user.AsDto();
        }
    }
}