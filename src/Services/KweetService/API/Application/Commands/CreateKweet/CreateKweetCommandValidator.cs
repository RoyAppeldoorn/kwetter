using FluentValidation;
using Kwetter.Services.KweetService.API.Infrastructure.Repositories;
using Kwetter.Services.KweetService.API.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Application.Commands.CreateKweet
{
    public class CreateKweetCommandValidator : AbstractValidator<CreateKweetCommand>
    {
        private readonly IKweetRepository _kweetRepository;

        public CreateKweetCommandValidator(IKweetRepository kweetRepository)
        {
            _kweetRepository = kweetRepository;

            RuleFor(m => m.KweetId)
                .CustomAsync(CreateKweetValidationAsync);
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.Message).NotEmpty().Length(0, 140);
        }

        private async Task CreateKweetValidationAsync(Guid KweetId, ValidationContext<CreateKweetCommand> context, CancellationToken cancellationToken)
        {
            Kweet kweet = await _kweetRepository.FindAsync(KweetId, cancellationToken);
            if (kweet != null)
                context.AddFailure("The kweet id already exists.");
        }
    }
}
