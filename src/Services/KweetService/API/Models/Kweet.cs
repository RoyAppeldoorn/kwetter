using Kwetter.Services.Common.API;
using System;
using System.Collections.Generic;

namespace Kwetter.Services.KweetService.API.Models
{
    public class Kweet : Entity
    {
        private List<KweetLike> _likes;

        public Guid UserId { get; init; }

        public string Message { get; init; }

        public DateTime CreatedDateTime { get; private set; }

        public ICollection<KweetLike> Likes => _likes;

        /// <summary>
        /// EF Constructor 
        /// </summary>
        protected Kweet() => _likes = new List<KweetLike>();

        public Kweet(Guid id, Guid userId, string message)
        {
            Id = id;
            UserId = userId;
            Message = message;
            CreatedDateTime = DateTime.UtcNow;
            _likes = new List<KweetLike>();
        }
    }
}
