using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testApii.DAL.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ValueListJson",
                table: "Santrals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueListJson",
                table: "Santrals");
        }
    }
}
