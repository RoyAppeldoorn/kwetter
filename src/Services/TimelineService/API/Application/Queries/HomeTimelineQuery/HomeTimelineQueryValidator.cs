using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.TimelineService.API.Application.Queries.HomeTimelineQuery
{
    public class HomeTimelineQueryValidator : AbstractValidator<HomeTimelineQuery>
    {
        public HomeTimelineQueryValidator()
        {
            RuleFor(homeTimelineQuery => homeTimelineQuery.UserId)
                .NotEmpty()
                .WithMessage("The user id cannot be empty.");
        }
    }
}
