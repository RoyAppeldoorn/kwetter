using FluentValidation;
using Kwetter.Services.UserService.API.Domain;
using Kwetter.Services.UserService.API.Infrastructure.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Application.Queries.GetProfileByName
{
    public class GetProfileByNameQueryValidator : AbstractValidator<GetProfileByNameQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetProfileByNameQueryValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

            RuleFor(getProfileByNameQuery => getProfileByNameQuery.Username)
                .NotEmpty()
                .WithMessage("The username cannot be empty.");

            RuleFor(getProfileByNameQuery => getProfileByNameQuery)
                .CustomAsync(ValidateUserAsync);
        }

        private async Task ValidateUserAsync(GetProfileByNameQuery getProfileByNameQuery, ValidationContext<GetProfileByNameQuery> context, CancellationToken cancellationToken)
        {
            User user = await _userRepository.FindByUserNameAsync(getProfileByNameQuery.Username, cancellationToken);
            if (user == default)
                context.AddFailure("The user does note exist.");
        }
    }
}
