using Kwetter.Services.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Domain
{
    public class Identity : Entity
    {
        public string OpenId { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }

        /// <summary>
        /// EF constructor...
        /// </summary>
        protected Identity() { }

        public Identity(Guid userid, string openId, string userName, string email)
        {
            Id = userid;
            OpenId = openId;
            UserName = userName.ToLower();
            Email = email;
        }
    }
}
