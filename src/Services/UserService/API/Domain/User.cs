using Kwetter.Services.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Domain
{
    public class User : Entity
    {
        public string Username { get; set; }

        /// <summary>
        /// Gets and sets the user profile.
        /// </summary>
        public UserProfile Profile { get; private set; }

        /// <summary>
        /// EF constructor...
        /// </summary>
        protected User() { }

        public User(Guid userId, string username)
        {
            Id = userId;
            Username = username;
            Profile = new UserProfile();
        }
    }
}
