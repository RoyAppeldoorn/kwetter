using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Application.Dtos
{
    public class ClaimDto
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public Claim ToClaim() => new(Name, Value);
    }
}
