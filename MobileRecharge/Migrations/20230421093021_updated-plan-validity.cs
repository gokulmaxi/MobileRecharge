using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileRecharge.Migrations
{
    public partial class updatedplanvalidity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CallBenifits",
                table: "RechargePlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataBenfits",
                table: "RechargePlans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallBenifits",
                table: "RechargePlans");

            migrationBuilder.DropColumn(
                name: "DataBenfits",
                table: "RechargePlans");
        }
    }
}
