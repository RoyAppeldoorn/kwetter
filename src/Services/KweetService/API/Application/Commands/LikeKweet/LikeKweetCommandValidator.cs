using FluentValidation;
using Kwetter.Services.KweetService.API.DataAccess.Repositories;
using Kwetter.Services.KweetService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.Commands.LikeKweet
{
    public class LikeKweetCommandValidator : AbstractValidator<LikeKweetCommand>
    {
        private readonly IKweetRepository _kweetRepository;

        public LikeKweetCommandValidator(IKweetRepository kweetRepository)
        {
            _kweetRepository = kweetRepository;

            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.KweetId).NotEmpty().CustomAsync(LikeKweetValidationAsync);
        }

        private async Task LikeKweetValidationAsync(Guid KweetId, ValidationContext<LikeKweetCommand> context, CancellationToken cancellationToken)
        {
            Kweet kweet = await _kweetRepository.FindAsync(KweetId, cancellationToken);
            if (kweet == null)
                context.AddFailure("The kweet does not exist.");
        }
    }
}
