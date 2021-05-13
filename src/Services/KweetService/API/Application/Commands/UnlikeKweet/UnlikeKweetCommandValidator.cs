using FluentValidation;
using Kwetter.Services.KweetService.API.Infrastructure.Repositories;
using Kwetter.Services.KweetService.API.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.Commands.UnlikeKweet
{
    public class UnlikeKweetCommandValidator : AbstractValidator<UnlikeKweetCommand>
    {
        private readonly IKweetRepository _kweetRepository;

        public UnlikeKweetCommandValidator(IKweetRepository kweetRepository)
        {
            _kweetRepository = kweetRepository;

            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.KweetId).NotEmpty().CustomAsync(UnlikeKweetValidationAsync);
        }

        private async Task UnlikeKweetValidationAsync(Guid KweetId, ValidationContext<UnlikeKweetCommand> context, CancellationToken cancellationToken)
        {
            Kweet kweet = await _kweetRepository.FindAsync(KweetId, cancellationToken);
            if (kweet == null)
                context.AddFailure("The kweet does not exist.");
        }
    }
}
