using FluentValidation;
using Kwetter.Services.FollowService.API.Domain;
using Kwetter.Services.FollowService.API.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Commands.DeleteFollowCommand
{
    public class DeleteFollowCommandValidator : AbstractValidator<DeleteFollowCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteFollowCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

            RuleFor(deleteFollowCommand => deleteFollowCommand.FollowerId)
                .NotEmpty();

            RuleFor(deleteFollowCommand => deleteFollowCommand.FollowingId)
                .NotEmpty();

            RuleFor(deleteFollowCommand => deleteFollowCommand)
                .CustomAsync(ValidateUnFollowAsync);
        }

        private async Task ValidateUnFollowAsync(DeleteFollowCommand deleteFollowCommand, ValidationContext<DeleteFollowCommand> context, CancellationToken cancellationToken)
        {
            User user = await _userRepository.FindByIdAsync(deleteFollowCommand.FollowerId, cancellationToken);
            bool hasFollow = user.Followings.Any(follow => follow.FollowingId == deleteFollowCommand.FollowingId);
            if (!hasFollow)
                context.AddFailure("The follow does not exist.");
        }
    }
}
