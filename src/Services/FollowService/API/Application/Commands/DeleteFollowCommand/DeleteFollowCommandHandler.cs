using Kwetter.Services.Common.API.CQRS;
using Kwetter.Services.FollowService.API.Domain;
using Kwetter.Services.FollowService.API.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Commands.DeleteFollowCommand
{
    public class DeleteFollowCommandHandler : IRequestHandler<DeleteFollowCommand, CommandResult>
    {
        private readonly IUserRepository _userRepository;

        public DeleteFollowCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<CommandResult> Handle(DeleteFollowCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.FindByIdAsync(request.FollowerId, cancellationToken);
            User otherUser = await _userRepository.FindByIdAsync(request.FollowingId, cancellationToken);
            bool unfollowed = user.Unfollow(otherUser);
            bool success = await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new CommandResult()
            {
                Success = unfollowed && success
            };
        }
    }
}
