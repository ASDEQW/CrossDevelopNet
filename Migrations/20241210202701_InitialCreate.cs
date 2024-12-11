using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OilCompanies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    industry = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OilCompanies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ITInfos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cms = table.Column<string>(type: "text", nullable: false),
                    externaljs = table.Column<string>(type: "text", nullable: false),
                    sociallinks = table.Column<string>(type: "text", nullable: false),
                    OilCompanyid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITInfos", x => x.id);
                    table.ForeignKey(
                        name: "FK_ITInfos_OilCompanies_OilCompanyid",
                        column: x => x.OilCompanyid,
                        principalTable: "OilCompanies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ITInfos_OilCompanyid",
                table: "ITInfos",
                column: "OilCompanyid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITInfos");

            migrationBuilder.DropTable(
                name: "OilCompanies");
        }
    }
}
