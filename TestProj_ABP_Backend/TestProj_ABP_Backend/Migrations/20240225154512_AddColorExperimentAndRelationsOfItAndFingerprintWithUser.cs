using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProj_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddColorExperimentAndRelationsOfItAndFingerprintWithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fingerprints",
                table: "Fingerprints");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceToken",
                table: "Fingerprints",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Fingerprints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Fingerprints",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAgent2",
                table: "Fingerprints",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fingerprints",
                table: "Fingerprints",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ColorTest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Group = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColorTest_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Fingerprints_Users_Id",
                table: "Fingerprints",
                column: "Id",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fingerprints_Users_Id",
                table: "Fingerprints");

            migrationBuilder.DropTable(
                name: "ColorTest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fingerprints",
                table: "Fingerprints");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Fingerprints");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Fingerprints");

            migrationBuilder.DropColumn(
                name: "UserAgent2",
                table: "Fingerprints");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceToken",
                table: "Fingerprints",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fingerprints",
                table: "Fingerprints",
                column: "DeviceToken");
        }
    }
}
