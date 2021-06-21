﻿using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.Common.Infrastructure.Messaging
{
    /// <summary>
    /// The default message handler. Emits the received message data as <see cref="byte[]"/>
    /// </summary>
    public interface IMessageHandler
    {
        Task HandleMessageAsync(string messageType, byte[] obj, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Typed variant of <see cref="IMessageHandler"/>. This serializes the <see cref="byte[]"/> of <see cref="IMessageHandler.HandleMessageAsync(string, byte[])"/> into <typeparamref name="TMessage"/>
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IMessageHandler<TMessage> : IMessageHandler
        where TMessage : class
    {
        /// <summary>
        /// Default interface implementation of <see cref="IMessageHandler.HandleMessageAsync(string, byte[])"/>
        /// </summary>
        Task IMessageHandler.HandleMessageAsync(string messageType, byte[] obj, CancellationToken cancellationToken)
        {
            return HandleMessageAsync(messageType, JsonSerializer.Deserialize<TMessage>(obj), cancellationToken);
        }

        Task HandleMessageAsync(string messageType, TMessage message, CancellationToken cancellationToken);
    }
}
