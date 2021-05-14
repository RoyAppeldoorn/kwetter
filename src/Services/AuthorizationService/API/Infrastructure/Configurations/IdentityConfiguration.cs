using Kwetter.Services.AuthorizationService.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.API.Infrastructure.Configurations
{
    public class IdentityConfiguration : IEntityTypeConfiguration<Identity>
    {
        public void Configure(EntityTypeBuilder<Identity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.OpenId)
                .IsRequired(true);

            builder.Property(p => p.Email)
                .IsRequired(true);

            builder.Property(p => p.UserName)
                .HasMaxLength(32)
                .IsRequired(true);

            builder.HasIndex(p => p.UserName)
                .IsUnique(true);
        }
    }
}
