using Kwetter.Services.FollowService.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kwetter.Services.FollowService.API.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Username)
                .IsRequired(true)
                .HasMaxLength(32);

            builder.HasMany(p => p.Followings)
                .WithOne(p => p.Follower)
                .HasForeignKey(p => p.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Followers)
                .WithOne(p => p.Following)
                .HasForeignKey(p => p.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Navigation(p => p.Followers)
                .HasField("followers")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
                .Navigation(p => p.Followings)
                .HasField("followings")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
