using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_SYSTEM.APPLICATION.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommitteeStaff",
                columns: table => new
                {
                    CommitteesId = table.Column<int>(type: "int", nullable: false),
                    StaffsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitteeStaff", x => new { x.CommitteesId, x.StaffsId });
                    table.ForeignKey(
                        name: "FK_CommitteeStaff_Committees_CommitteesId",
                        column: x => x.CommitteesId,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommitteeStaff_STAFF_StaffsId",
                        column: x => x.StaffsId,
                        principalTable: "STAFF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommitteeStudent",
                columns: table => new
                {
                    CommitteesId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitteeStudent", x => new { x.CommitteesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CommitteeStudent_Committees_CommitteesId",
                        column: x => x.CommitteesId,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommitteeStudent_STUDENTS_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "STUDENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeStaff_StaffsId",
                table: "CommitteeStaff",
                column: "StaffsId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeStudent_StudentsId",
                table: "CommitteeStudent",
                column: "StudentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommitteeStaff");

            migrationBuilder.DropTable(
                name: "CommitteeStudent");
        }
    }
}
