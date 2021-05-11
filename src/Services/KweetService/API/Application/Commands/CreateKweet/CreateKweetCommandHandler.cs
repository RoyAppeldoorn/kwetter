using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.KweetService.API.DataAccess;
using Kwetter.Services.KweetService.API.DataAccess.Repositories;
using Kwetter.Services.KweetService.API.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.Commands.CreateKweet
{
    public class CreateKweetCommandHandler : IRequestHandler<CreateKweetCommand, CommandResult>
    {
        private readonly IKweetRepository _kweetRepository;

        public CreateKweetCommandHandler(IKweetRepository kweetRepository, KweetDbContext kweetDbContext)
        {
            _kweetRepository = kweetRepository ?? throw new ArgumentNullException(nameof(kweetRepository));
        }

        public async Task<CommandResult> Handle(CreateKweetCommand request, CancellationToken cancellationToken)
        {
            Kweet kweet = new(request.KweetId, request.UserId, request.Message);
            Kweet trackedKweet = _kweetRepository.Create(kweet);
            bool success = await _kweetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new CommandResult { 
                Success = trackedKweet != null && success 
            };
        }
    }
}
