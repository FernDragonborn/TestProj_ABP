using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProj_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceTokenToColorTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceToken",
                table: "ColorTest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceToken",
                table: "ColorTest");
        }
    }
}
