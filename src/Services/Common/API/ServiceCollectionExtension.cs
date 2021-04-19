using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Kwetter.Services.Common.API.CQRS.Behavior;
using FluentValidation;

namespace Kwetter.Services.Common.API
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDefaultApplicationServices(this IServiceCollection serviceCollection, Assembly applicationAssembly)
        {
            serviceCollection.AddAutoMapper(applicationAssembly);
            serviceCollection.AddMediatR(applicationAssembly);
            serviceCollection.AddValidatorsFromAssembly(applicationAssembly);
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            return serviceCollection;
        }
    }
}
