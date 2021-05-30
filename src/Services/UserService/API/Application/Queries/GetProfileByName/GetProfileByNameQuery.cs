using Kwetter.Services.Common.API.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Application.Queries.GetProfileByName
{
    public record GetProfileByNameQuery : IRequest<QueryResponse<ProfileDto>>
    {
        public string Username { get; set; }
    }
}
