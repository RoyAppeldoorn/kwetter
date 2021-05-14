using Kwetter.Services.Common.API.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Application.Commands.SetCustomClaims
{
    public record SetCustomClaimsCommand(string IdToken, string UserName) : IRequest<CommandResult>;
}
