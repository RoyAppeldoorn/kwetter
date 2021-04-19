using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kwetter.Services.KweetService.API.DataAccess.Migrations
{
    public partial class KweetWithLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KweetLike",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    KweetId = table.Column<byte[]>(type: "varbinary(16)", nullable: true),
                    UserId = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    LikedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KweetLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KweetLike_Kweets_KweetId",
                        column: x => x.KweetId,
                        principalTable: "Kweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KweetLike_KweetId",
                table: "KweetLike",
                column: "KweetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KweetLike");
        }
    }
}
