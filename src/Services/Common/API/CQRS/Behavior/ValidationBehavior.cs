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
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
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

            _logger.LogInformation($"{_validatorName} started.");
            ValidationContext<TRequest> context = new(request);
            List<ValidationFailure> failures = new();

            foreach (IValidator<TRequest> validator in _validators)
            {
                ValidationResult validationResult = await validator.ValidateAsync(context, cancellationToken);
                if (!validationResult.IsValid)
                    failures.AddRange(validationResult.Errors);
            }

            if (failures.Count != 0)
            {
                _logger.LogWarning($"{_validatorName}: completed with {failures.Count} validation failure{(failures.Count > 1 ? "s" : "")}.");
                throw new ValidationException($"Command Validation Errors for type {typeof(TRequest).Name}", failures);
            }

            _logger.LogInformation($"{_validatorName} completed.");
            return await next();
        }
    }
}
