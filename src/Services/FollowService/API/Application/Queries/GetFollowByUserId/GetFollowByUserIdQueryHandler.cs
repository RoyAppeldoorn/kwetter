using AutoMapper;
using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.FollowService.API.Domain;
using Kwetter.Services.FollowService.API.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Queries.GetFollowByUserId
{
    public class GetFollowByUserIdQueryHandler : IRequestHandler<GetFollowByUserIdQuery, QueryResponse<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetFollowByUserIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<QueryResponse<UserDto>> Handle(GetFollowByUserIdQuery request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.FindByIdAsync(request.UserId, cancellationToken);

            UserDto userDto = new()
            {
                Id = user.Id,
                Username = user.Username,
                Followers = user.Followers.Select(x => new FollowDto { Id = x.FollowerId, Username = x.Follower.Username }).ToList(),
                Following = user.Followings.Select(x => new FollowDto { Id = x.FollowingId, Username = x.Following.Username }).ToList(),
            };

            return new QueryResponse<UserDto>()
            {
                Data = userDto,
                Success = true
            };
        }
    }
}
