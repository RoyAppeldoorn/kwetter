using FluentValidation;
using Kwetter.Services.AuthorizationService.API.Domain;
using Kwetter.Services.AuthorizationService.API.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Application.Commands.SetCustomClaims
{
    public class SetCustomClaimsCommandValidator : AbstractValidator<SetCustomClaimsCommand>
    {
        private readonly IIdentityRepository _identityRepository;

        public SetCustomClaimsCommandValidator(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;

            RuleFor(claimsCommand => claimsCommand.IdToken)
                .NotEmpty()
                .WithMessage("The id token cannot be empty.");

            RuleFor(claimsCommand => claimsCommand.UserName)
                .NotEmpty().WithMessage("The username cannot be empty.")
                .MaximumLength(32).WithMessage("The username cannot exceed the 32 characters limit.")
                .CustomAsync(UserNameValidation);
        }

        private async Task UserNameValidation(string userName, ValidationContext<SetCustomClaimsCommand> context, CancellationToken cancellationToken)
        {
            Identity identity = await _identityRepository.FindByUserNameAsync(userName, cancellationToken);
            if (identity != default)
            {
                context.AddFailure("The username is not unique.");
                return;
            }
        }
    }
}
