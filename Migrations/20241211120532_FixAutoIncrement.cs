using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class FixAutoIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ITInfos_OilCompanies_OilCompanyid",
                table: "ITInfos");

            migrationBuilder.DropIndex(
                name: "IX_ITInfos_OilCompanyid",
                table: "ITInfos");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "ITInfos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_ITInfos_OilCompanies_id",
                table: "ITInfos",
                column: "id",
                principalTable: "OilCompanies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ITInfos_OilCompanies_id",
                table: "ITInfos");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "ITInfos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_ITInfos_OilCompanyid",
                table: "ITInfos",
                column: "OilCompanyid");

            migrationBuilder.AddForeignKey(
                name: "FK_ITInfos_OilCompanies_OilCompanyid",
                table: "ITInfos",
                column: "OilCompanyid",
                principalTable: "OilCompanies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
