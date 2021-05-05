using Kwetter.Services.KweetService.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kwetter.Services.KweetService.API.DataAccess.Configuration
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

                navigationBuilder.Property(p => p.LikedDateTime)
                    .IsRequired(true);

                navigationBuilder.WithOwner();
            })
            .Navigation(p => p.Likes)
            .HasField("_likes")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
