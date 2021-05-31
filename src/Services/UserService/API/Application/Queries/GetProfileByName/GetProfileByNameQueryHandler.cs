using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.UserService.API.Domain;
using Kwetter.Services.UserService.API.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Application.Queries.GetProfileByName
{
    public class GetProfileByNameQueryHandler : IRequestHandler<GetProfileByNameQuery, QueryResponse<ProfileDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetProfileByNameQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<QueryResponse<ProfileDto>> Handle(GetProfileByNameQuery request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.FindByUserNameAsync(request.Username, cancellationToken);

            // We already know the user exists so no need to check for empty user
            if(user.Profile != default)
            {
                return new QueryResponse<ProfileDto>()
                {
                    Data = new ProfileDto()
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Bio = user.Profile.Bio,
                        PictureUrl = user.Profile.PictureUrl,
                        Location = user.Profile.Location
                    },
                    Success = true
                };
            }

            return new QueryResponse<ProfileDto>()
            {
                Data = new ProfileDto()
                {
                    Id = user.Id,
                    Username = user.Username,
                },
                Success = true
            };
        }
    }
}
