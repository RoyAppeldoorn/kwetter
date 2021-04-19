using Kwetter.Services.Common.API.CQRS;
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

        public CreateKweetCommandHandler(IKweetRepository kweetRepository)
        {
            _kweetRepository = kweetRepository ?? throw new ArgumentNullException(nameof(kweetRepository));
        }

        public async Task<CommandResult> Handle(CreateKweetCommand request, CancellationToken cancellationToken)
        {
            var kweet = new Kweet
            {
                Id = request.KweetId,
                Message = request.Message,
                UserId = request.UserId
            };

            var success = await _kweetRepository.CreateKweet(kweet);
            return new CommandResult { Success = success };
        }
    }
}
