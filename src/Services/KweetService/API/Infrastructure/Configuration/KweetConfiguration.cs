using Kwetter.Services.KweetService.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kwetter.Services.KweetService.API.Infrastructure.Configuration
{
    public class KweetConfiguration : IEntityTypeConfiguration<Kweet>
    {
        public void Configure(EntityTypeBuilder<Kweet> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId)
                .IsRequired(true);

            builder.Property(p => p.CreatedDateTime)
                .IsRequired(true);

            builder.Property(p => p.Message)
                .HasMaxLength(140)
                .IsRequired(true);

            builder.OwnsMany(p => p.Likes, navigationBuilder =>
            {
                navigationBuilder.Ignore(p => p.Id);

                navigationBuilder.HasKey(p => new { p.KweetId, p.UserId });

                navigationBuilder.Property(p => p.KweetId).ValueGeneratedNever();

                navigationBuilder.Property(p => p.UserId).ValueGeneratedNever();

                navigationBuilder.Property(p => p.Id).ValueGeneratedNever();

                navigationBuilder.Property(p => p.LikedDateTime)
                    .IsRequired(true);

                navigationBuilder.WithOwner();
            });
        }
    }
}
