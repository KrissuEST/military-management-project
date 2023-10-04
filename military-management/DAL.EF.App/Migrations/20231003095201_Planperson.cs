using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.App.Migrations
{
    /// <inheritdoc />
    public partial class Planperson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRefreshToken_AspNetUsers_AppUserId",
                table: "AppRefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRefreshToken",
                table: "AppRefreshToken");

            migrationBuilder.RenameTable(
                name: "AppRefreshToken",
                newName: "AppRefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_AppRefreshToken_AppUserId",
                table: "AppRefreshTokens",
                newName: "IX_AppRefreshTokens_AppUserId");

            migrationBuilder.AddColumn<string>(
                name: "PlanDescription",
                table: "MilitaryPlans",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlanLocation",
                table: "MilitaryPlans",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRefreshTokens",
                table: "AppRefreshTokens",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PlanPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonFirstName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PersonLastName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PersonBirthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PersonIdNumber = table.Column<long>(type: "bigint", nullable: false),
                    PersonExtraInfo = table.Column<string>(type: "text", nullable: false),
                    MilitaryPlanId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanPersons_MilitaryPlans_MilitaryPlanId",
                        column: x => x.MilitaryPlanId,
                        principalTable: "MilitaryPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanPersons_MilitaryPlanId",
                table: "PlanPersons",
                column: "MilitaryPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRefreshTokens_AspNetUsers_AppUserId",
                table: "AppRefreshTokens",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRefreshTokens_AspNetUsers_AppUserId",
                table: "AppRefreshTokens");

            migrationBuilder.DropTable(
                name: "PlanPersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRefreshTokens",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "PlanDescription",
                table: "MilitaryPlans");

            migrationBuilder.DropColumn(
                name: "PlanLocation",
                table: "MilitaryPlans");

            migrationBuilder.RenameTable(
                name: "AppRefreshTokens",
                newName: "AppRefreshToken");

            migrationBuilder.RenameIndex(
                name: "IX_AppRefreshTokens_AppUserId",
                table: "AppRefreshToken",
                newName: "IX_AppRefreshToken_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRefreshToken",
                table: "AppRefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRefreshToken_AspNetUsers_AppUserId",
                table: "AppRefreshToken",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
