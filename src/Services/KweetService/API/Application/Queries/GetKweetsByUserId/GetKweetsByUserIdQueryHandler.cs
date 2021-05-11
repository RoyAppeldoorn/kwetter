using AutoMapper;
using Kwetter.Services.KweetService.API.DataAccess.Repositories;
using Kwetter.Services.KweetService.API.Dto.Kweet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.Queries.GetKweetsByUserId
{
    public class GetKweetsByUserIdQueryHandler : IRequestHandler<GetKweetsByUserIdQuery, List<KweetDto>>
    {
        private readonly IKweetRepository _kweetRepository;
        private readonly IMapper _mapper;

        public GetKweetsByUserIdQueryHandler(IKweetRepository kweetRepository, IMapper mapper)
        {
            _kweetRepository = kweetRepository ?? throw new ArgumentNullException(nameof(kweetRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<KweetDto>> Handle(GetKweetsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _kweetRepository.FindKweetsByUserIdAsync(request.UserId, cancellationToken);
            return results.Select(result => _mapper.Map<KweetDto>(result)).ToList();
        }
    }
}
