using FluentValidation;
using Kwetter.Services.FollowService.API.Domain;
using Kwetter.Services.FollowService.API.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Commands.CreateFollowCommand
{
    public class CreateFollowCommandValidator : AbstractValidator<CreateFollowCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateFollowCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

            RuleFor(createFollowCommand => createFollowCommand.FollowerId)
                .NotEmpty();

            RuleFor(createFollowCommand => createFollowCommand.FollowingId)
                .NotEmpty();

            RuleFor(createFollowCommand => createFollowCommand)
                .CustomAsync(ValidateFollowAsync);
        }

        private async Task ValidateFollowAsync(CreateFollowCommand createFollowCommand, ValidationContext<CreateFollowCommand> context, CancellationToken cancellationToken)
        {
            if (createFollowCommand.FollowingId == createFollowCommand.FollowerId)
            {
                context.AddFailure("The follower cannot follow himself.");
                return;
            }
            User user = await _userRepository.FindByIdAsync(createFollowCommand.FollowerId, cancellationToken);
            if (user.Followings.Any(follow => follow.FollowingId == createFollowCommand.FollowingId))
                context.AddFailure("The user is already followed.");
        }
    }
}
