using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompraloYa.Migrations
{
    public partial class CambiandoImagen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Joyerias");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Img",
                table: "Joyerias",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
