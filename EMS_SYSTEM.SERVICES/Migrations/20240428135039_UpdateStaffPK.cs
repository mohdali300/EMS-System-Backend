using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_SYSTEM.APPLICATION.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStaffPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "STAFF",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
            migrationBuilder.AddPrimaryKey(
                name: "PK_STAFF",
                table: "STAFF",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_STAFF",
                table: "STAFF");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "STAFF",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");        }
    }
}
