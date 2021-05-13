using Kwetter.Services.Common.Domain;

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
