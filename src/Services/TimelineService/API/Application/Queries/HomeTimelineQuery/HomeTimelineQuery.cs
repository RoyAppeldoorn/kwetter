using Kwetter.Services.Common.API.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.Queries.HomeTimelineQuery
{
    public record HomeTimelineQuery : IRequest<QueryResponse<IEnumerable<KweetDto>>>
    {
        public Guid UserId { get; set; }

        public uint PageNumber { get; set; }

        public uint PageSize { get; set; }
    }
}
