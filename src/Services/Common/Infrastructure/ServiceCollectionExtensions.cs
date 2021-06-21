using Kwetter.Services.Common.Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.Common.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMessagePublishing(this IServiceCollection services, string queueName, Action<MessagingBuilder> builderFn = null)
        {
            var builder = new MessagingBuilder(services);
            services.AddHostedService<QueueReaderService>();
            services.AddSingleton(new MessageHandlerRepository(builder.MessageHandlers));

            builderFn?.Invoke(builder);
            var queueNameService = new QueueName(queueName);
            services.AddSingleton(queueNameService);
            var connection = new RabbitMqConnection();
            services.AddSingleton(connection);
            services.AddScoped<IMessagePublisher, RabbitMqMessagePublisher>();
        }
    }
}
