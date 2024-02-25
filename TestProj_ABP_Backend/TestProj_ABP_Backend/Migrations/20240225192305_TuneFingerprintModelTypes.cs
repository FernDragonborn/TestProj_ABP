using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProj_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class TuneFingerprintModelTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAgent2",
                table: "Fingerprints");

            migrationBuilder.AlterColumn<float>(
                name: "PixelRatio",
                table: "Fingerprints",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "OnlineStatus",
                table: "Fingerprints",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PixelRatio",
                table: "Fingerprints",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OnlineStatus",
                table: "Fingerprints",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAgent2",
                table: "Fingerprints",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
