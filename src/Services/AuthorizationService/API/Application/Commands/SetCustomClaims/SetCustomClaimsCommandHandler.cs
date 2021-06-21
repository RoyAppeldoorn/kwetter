using Kwetter.Services.AuthorizationService.API.Application.DomainEvents;
using Kwetter.Services.AuthorizationService.API.Application.Dtos;
using Kwetter.Services.AuthorizationService.API.Domain;
using Kwetter.Services.AuthorizationService.API.Infrastructure.Repositories;
using Kwetter.Services.AuthorizationService.API.Infrastructure.Services;
using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.Common.Infrastructure.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Application.Commands.SetCustomClaims
{
    public class SetCustomClaimsCommandHandler : IRequestHandler<SetCustomClaimsCommand, CommandResult>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMessagePublisher _messagePublisher;

        public SetCustomClaimsCommandHandler(IAuthorizationService authorizationService, IIdentityRepository identityRepository, IMessagePublisher messagePublisher)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        }

        public async Task<CommandResult> Handle(SetCustomClaimsCommand request, CancellationToken cancellationToken)
        {
            CommandResult commandResult = new();

            TokenDto tokenDto = await _authorizationService.VerifyIdTokenAsync(request.IdToken, cancellationToken);
            string openId = tokenDto.Subject;
            string email = tokenDto.Claims["email"].Value;

            Identity identity = await _identityRepository.FindIdentityByOpenIdAsync(openId, cancellationToken);
            if(identity == default)
            {
                Identity newIdentity = new(Guid.NewGuid(), openId, request.UserName, email);
                Dictionary<string, object> claims = new()
                {
                    { "UserId", newIdentity.Id },
                    { "UserName", newIdentity.UserName },
                    { "User", true }
                };
                await _authorizationService.SetUserClaimsAsync(openId, claims, cancellationToken);
                identity = _identityRepository.Create(newIdentity);
                bool success = await _identityRepository.UnitOfWork.SaveEntitiesAsync();

                if(success)
                {
                    // publish domain event to rabbitmq
                    IdentityCreatedDomainEvent identityCreatedDomainEvent = new(userId: newIdentity.Id, userName: newIdentity.UserName);
                    await _messagePublisher.PublishMessageAsync("IdentityCreated", identityCreatedDomainEvent);
                }
            }

            commandResult.Success = identity != default;
            return commandResult;
        }
    }
}
