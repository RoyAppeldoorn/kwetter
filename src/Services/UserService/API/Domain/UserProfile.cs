using Kwetter.Services.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.UserService.API.Domain
{
    public class UserProfile : Entity
    {
        public string Bio { get; private set; }

        public string PictureUrl { get; private set; }

        public string Location { get; private set; }

        /// <summary>
        /// EF constructor...
        /// </summary>
        public UserProfile() { }
    }
}
