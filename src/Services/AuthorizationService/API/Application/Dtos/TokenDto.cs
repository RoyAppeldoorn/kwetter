using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Application.Dtos
{
    public class TokenDto
    {
        public string Subject { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public long ExpirationTimeSeconds { get; set; }

        public long IssuedAtTimeSeconds { get; set; }

        public IReadOnlyDictionary<string, ClaimDto> Claims { get; set; }
    }
}
