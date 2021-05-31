using FluentValidation;
using Kwetter.Services.FollowService.API.Domain;
using Kwetter.Services.FollowService.API.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API.Application.Queries.GetFollowByUserId
{
    public class GetFollowByUserIdQueryValidator : AbstractValidator<GetFollowByUserIdQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetFollowByUserIdQueryValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

            RuleFor(getFollowByUserIdQuery => getFollowByUserIdQuery.UserId)
                .NotEmpty()
                .WithMessage("The user id cannot be empty.");

            RuleFor(getFollowByUserIdQuery => getFollowByUserIdQuery)
                .CustomAsync(ValidateUserAsync);
        }

        private async Task ValidateUserAsync(GetFollowByUserIdQuery getFollowByUserIdQuery, ValidationContext<GetFollowByUserIdQuery> context, CancellationToken cancellationToken)
        {
            User user = await _userRepository.FindByIdAsync(getFollowByUserIdQuery.UserId, cancellationToken);
            if (user == default)
                context.AddFailure("The user does note exist.");
        }
    }
}
