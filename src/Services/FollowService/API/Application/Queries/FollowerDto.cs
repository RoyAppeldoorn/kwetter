using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Queries
{
    public record FollowerDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
    }
}
