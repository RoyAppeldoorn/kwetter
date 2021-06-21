using Kwetter.Services.FollowService.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kwetter.Services.FollowService.API.Infrastructure.Configurations
{
    internal sealed class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {         
            builder.Ignore(p => p.Id);
          
            builder.HasKey(p => new { p.FollowingId, p.FollowerId });

            builder.Property(p => p.FollowerId)
                .IsRequired(true);

            builder.Property(p => p.FollowingId)
                .IsRequired(true);

            builder.Property(p => p.FollowDateTime)
                .IsRequired(true);
        }
    }
}
