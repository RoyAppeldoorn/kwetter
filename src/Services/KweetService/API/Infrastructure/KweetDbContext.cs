using Kwetter.Services.Common.Infrastructure;
using Kwetter.Services.KweetService.API.Infrastructure.Configuration;
using Kwetter.Services.KweetService.API.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.API.Infrastructure
{
    public class KweetDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Kweet> Kweets { get; set; }

        public KweetDbContext(DbContextOptions<KweetDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KweetConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
