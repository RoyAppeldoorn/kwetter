using FluentValidation;

namespace Kwetter.Services.KweetService.API.Application.Commands.CreateKweet
{
    public class CreateKweetValidator : AbstractValidator<CreateKweetCommand>
    {
        public CreateKweetValidator()
        {
            RuleFor(m => m.KweetId).NotEmpty();
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.Message).NotEmpty().Length(0, 150);
        }
    }
}
