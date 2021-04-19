using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.Common.API.CQRS.Behavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TResponse : CommandResult, new()
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly string _validatorName;
        private readonly bool _anyValidators;

        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
            _anyValidators = _validators.Any();

            if (_anyValidators)
                _validatorName = _validators.First().GetType().Name;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_anyValidators)
                return await next();

            _logger.LogInformation($"{_validatorName}: Started.");

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Any())
            {
                _logger.LogWarning($"{_validatorName}: Completed. Validation Errors for type {typeof(TRequest).Name}", failures);
                TResponse response = new();
                response.Errors.AddRange(failures.Select(validationFailure => validationFailure.ErrorMessage));
                return response;
            }

            _logger.LogInformation($"{_validatorName}: Completed with 0 errors.");
            return await next();
        }
    }
}
