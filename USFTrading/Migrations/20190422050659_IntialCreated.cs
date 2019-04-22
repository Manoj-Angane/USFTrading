using Microsoft.EntityFrameworkCore.Migrations;

namespace USFTrading.Migrations
{
    public partial class IntialCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    symbol = table.Column<string>(nullable: false),
                    companyName = table.Column<string>(nullable: true),
                    exchange = table.Column<string>(nullable: true),
                    industry = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    CEO = table.Column<string>(nullable: true),
                    sector = table.Column<string>(nullable: true),
                    Gainer_Loser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.symbol);
                });

            migrationBuilder.CreateTable(
                name: "KeyStatGainers",
                columns: table => new
                {
                    symbol = table.Column<string>(nullable: false),
                    marketcap = table.Column<double>(nullable: false),
                    week52high = table.Column<float>(nullable: false),
                    week52low = table.Column<float>(nullable: false),
                    week52change = table.Column<float>(nullable: false),
                    ttmEPS = table.Column<float>(nullable: false),
                    ttmDividendRate = table.Column<float>(nullable: false),
                    peRatio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyStatGainers", x => x.symbol);
                });

            migrationBuilder.CreateTable(
                name: "KeyStatLosers",
                columns: table => new
                {
                    symbol = table.Column<string>(nullable: false),
                    marketcap = table.Column<double>(nullable: false),
                    week52high = table.Column<float>(nullable: false),
                    week52low = table.Column<float>(nullable: false),
                    week52change = table.Column<float>(nullable: false),
                    ttmEPS = table.Column<float>(nullable: false),
                    ttmDividendRate = table.Column<float>(nullable: false),
                    peRatio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyStatLosers", x => x.symbol);
                });

            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    type = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false),
                    performance = table.Column<float>(nullable: false),
                    lastUpdated = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    symbol = table.Column<string>(nullable: false),
                    close = table.Column<float>(nullable: false),
                    previousClose = table.Column<float>(nullable: false),
                    change = table.Column<float>(nullable: true),
                    changePercent = table.Column<float>(nullable: false),
                    marketCap = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.symbol);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "KeyStatGainers");

            migrationBuilder.DropTable(
                name: "KeyStatLosers");

            migrationBuilder.DropTable(
                name: "Sector");

            migrationBuilder.DropTable(
                name: "Stock");
        }
    }
}
