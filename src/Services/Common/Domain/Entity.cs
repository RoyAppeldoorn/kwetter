using System;

namespace Kwetter.Services.Common.Domain
{
    public abstract class Entity
    {
        /// <summary>
        /// Gets and sets the id of the entity.
        /// </summary>
        public Guid Id { get; protected set; }
    }
}
