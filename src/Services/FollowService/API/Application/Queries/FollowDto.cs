using Kwetter.Services.FollowService.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Queries
{
    public record FollowDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public List<FollowerDto> Followers { get; set; }

        public List<FollowerDto> Following { get; set; }
    }
}
