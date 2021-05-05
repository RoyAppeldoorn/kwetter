using Kwetter.Services.Common.API;
using System;
using System.Collections.Generic;

namespace Kwetter.Services.KweetService.API.Models
{
    public class Kweet : Entity
    {
        public Guid UserId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public List<KweetLike> Likes { get; set; }

        /// <summary>
        /// EF Constructor 
        /// </summary>
        protected Kweet() => Likes = new List<KweetLike>();

        public Kweet(Guid id, Guid userId, string message)
        {
            Id = id;
            UserId = userId;
            Message = message;
            CreatedDateTime = DateTime.UtcNow;
            Likes = new List<KweetLike>();
        }

        public void AddLike(Guid userId)
        {
            KweetLike like = new(Id, userId);
            Likes.Add(like);
        }
    }
}
