using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kwetter.Services.KweetService.API.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kweets",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    UserId = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    Message = table.Column<string>(type: "varchar(140)", maxLength: 140, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kweets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KweetLike",
                columns: table => new
                {
                    KweetId = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    UserId = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    LikedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KweetLike", x => new { x.KweetId, x.UserId });
                    table.ForeignKey(
                        name: "FK_KweetLike_Kweets_KweetId",
                        column: x => x.KweetId,
                        principalTable: "Kweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KweetLike");

            migrationBuilder.DropTable(
                name: "Kweets");
        }
    }
}
