using MediatR;
using System;
using System.Collections.Generic;

namespace Kwetter.Services.KweetService.API.Application.Queries.GetKweetsByUserId
{
    public record GetKweetsByUserIdQuery(Guid UserId) : IRequest<List<KweetDto>>;
}
