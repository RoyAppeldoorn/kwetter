using Kwetter.Services.UserService.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kwetter.Services.UserService.API.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Username)
                .IsRequired(true)
                .HasMaxLength(32);

            builder.HasIndex(p => p.Username)
                .IsUnique(true);

            builder.OwnsOne(p => p.Profile, navigationBuilder =>
            {
                navigationBuilder.Ignore(p => p.Id);

                navigationBuilder.Property(p => p.Bio);

                navigationBuilder.Property(p => p.Location);

                navigationBuilder.Property(p => p.PictureUrl);

                navigationBuilder.WithOwner();
            });
        }
    }
}
