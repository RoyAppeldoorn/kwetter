using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kwetter.Services.KweetService.API.DataAccess.Migrations
{
    public partial class NeverGenerateValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "KweetLike",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "KweetLike");
        }
    }
}
