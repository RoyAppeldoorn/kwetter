﻿using FluentValidation;
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
    public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : Response, new()
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionBehaviour{TRequest,TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc cref="IPipelineBehavior{TRequest,TResponse}.Handle(TRequest, CancellationToken, RequestHandlerDelegate{TResponse})"/>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (ValidationException validationException)
            {
                TResponse response = new();
                response.Errors.AddRange(
                    validationException.Errors.Select(validationFailure => validationFailure.ErrorMessage));
                return response;
            }
            catch (Exception ex)
            {
                string requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Unhandled Exception during Request {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}
