using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_SYSTEM.APPLICATION.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PalceId",
                table: "Committees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Committees_PalceId",
                table: "Committees",
                column: "PalceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Committees_PALCES_PalceId",
                table: "Committees",
                column: "PalceId",
                principalTable: "PALCES",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Committees_PALCES_PalceId",
                table: "Committees");

            migrationBuilder.DropIndex(
                name: "IX_Committees_PalceId",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "PalceId",
                table: "Committees");
        }
    }
}
