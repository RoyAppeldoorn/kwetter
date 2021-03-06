// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Kwetter.Services.KweetService.API.Infrastructure.Migrations
{
    [DbContext(typeof(KweetDbContext))]
    partial class KweetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Kwetter.Services.KweetService.API.Models.Kweet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("varchar(140)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Kweets");
                });

            modelBuilder.Entity("Kwetter.Services.KweetService.API.Models.Kweet", b =>
                {
                    b.OwnsMany("Kwetter.Services.KweetService.API.Models.KweetLike", "Likes", b1 =>
                        {
                            b1.Property<Guid>("KweetId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Id")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("LikedDateTime")
                                .HasColumnType("datetime(6)");

                            b1.HasKey("KweetId", "UserId");

                            b1.ToTable("KweetLike");

                            b1.WithOwner()
                                .HasForeignKey("KweetId");
                        });

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
