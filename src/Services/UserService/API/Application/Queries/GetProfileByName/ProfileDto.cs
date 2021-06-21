using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Application.Queries.GetProfileByName
{
    public record ProfileDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Bio { get; set; }

        public string PictureUrl { get; set; }

        public string Location { get; set; }
    }
}
