using Kwetter.Services.Common.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.Common.Infrastructure
{
    public interface IRepository<T> where T : Entity
    {
        /// <summary>
        /// Gets the unit of work for the repository.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }
    }
}
