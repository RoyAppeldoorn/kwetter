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
    public class GetFollowByUserIdQueryHandler : IRequestHandler<GetFollowByUserIdQuery, QueryResponse<FollowDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetFollowByUserIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<QueryResponse<FollowDto>> Handle(GetFollowByUserIdQuery request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.FindByIdAsync(request.UserId, cancellationToken);

            FollowDto followDto = new()
            {
                Id = user.Id,
                Username = user.Username,
                Followers = user.Followers.Select(x => new FollowerDto { Id = x.FollowerId, Username = x.Follower.Username }).ToList(),
                Following = user.Followings.Select(x => new FollowerDto { Id = x.FollowingId, Username = x.Following.Username }).ToList(),
            };

            return new QueryResponse<FollowDto>()
            {
                Data = followDto,
                Success = true
            };
        }
    }
}
