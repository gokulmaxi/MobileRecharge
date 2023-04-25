using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileRecharge.Migrations
{
    public partial class addedrechargeReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RechargeReport",
                columns: table => new
                {
                    RechargeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    RechargedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeReport", x => x.RechargeId);
                    table.ForeignKey(
                        name: "FK_RechargeReport_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RechargeReport_RechargePlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "RechargePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RechargeReport_PlanId",
                table: "RechargeReport",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeReport_UserId",
                table: "RechargeReport",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RechargeReport");
        }
    }
}
