using AutoMapper;
using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.TimelineService.API.Domain;
using Kwetter.Services.TimelineService.API.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.Queries.HomeTimelineQuery
{
    public class HomeTimelineQueryHandler : IRequestHandler<HomeTimelineQuery, QueryResponse<IEnumerable<KweetDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ITimelineGraphRepository _timelineGraphRepository;

        public HomeTimelineQueryHandler(IMapper mapper, ITimelineGraphRepository timelineGraphRepository)
        {
            _timelineGraphRepository = timelineGraphRepository ?? throw new ArgumentNullException(nameof(timelineGraphRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<QueryResponse<IEnumerable<KweetDto>>> Handle(HomeTimelineQuery request, CancellationToken cancellationToken)
        {
            Timeline timeline = await _timelineGraphRepository.GetPaginatedTimelineAsync(request.UserId, request.PageNumber, request.PageSize);
            QueryResponse<IEnumerable<KweetDto>> queryResponse = new();
            queryResponse.Data = _mapper.Map<IEnumerable<KweetDto>>(timeline.Kweets);
            queryResponse.Success = true;
            return queryResponse;
        }
    }
}
