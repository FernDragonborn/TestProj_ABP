using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProj_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddFingerprints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fingerprints",
                columns: table => new
                {
                    DeviceToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenWidth = table.Column<int>(type: "int", nullable: true),
                    ScreenHeight = table.Column<int>(type: "int", nullable: true),
                    ColorDepth = table.Column<int>(type: "int", nullable: true),
                    PixelRatio = table.Column<int>(type: "int", nullable: true),
                    Orintation = table.Column<int>(type: "int", nullable: true),
                    BrowserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrowserVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Os = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnlineStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plugins = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCookieEnabled = table.Column<bool>(type: "bit", nullable: true),
                    DefaultLocale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollatorInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListFormatInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelativeTimeFormatInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PluralRulesInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fingerprints", x => x.DeviceToken);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fingerprints");
        }
    }
}
