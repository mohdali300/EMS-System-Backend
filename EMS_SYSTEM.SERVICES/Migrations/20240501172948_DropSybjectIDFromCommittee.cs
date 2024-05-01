using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_SYSTEM.APPLICATION.Migrations
{
    /// <inheritdoc />
    public partial class DropSybjectIDFromCommittee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectID",
                table: "Committees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectID",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
