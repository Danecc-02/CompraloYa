using Microsoft.EntityFrameworkCore.Migrations;

namespace CompraloYa.Migrations
{
    public partial class ActualizandoControllers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Tecnologias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Ropas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Tecnologias");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Ropas");
        }
    }
}
