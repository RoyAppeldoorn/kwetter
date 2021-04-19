using Kwetter.Services.KweetService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.KweetService.API.DataAccess
{
    public class KweetDbContext : DbContext
    {
        public DbSet<Kweet> Kweets { get; set; }

        public KweetDbContext(DbContextOptions<KweetDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KweetLike>()
                .HasOne(k => k.Kweet)
                .WithMany(k => k.Likes);
        }
    }
}
