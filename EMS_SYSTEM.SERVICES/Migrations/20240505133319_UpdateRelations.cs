using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_SYSTEM.APPLICATION.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommitteeStaff");

            migrationBuilder.DropTable(
                name: "CommitteeStudent");

            migrationBuilder.CreateTable(
                name: "StaffCommittees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffID = table.Column<int>(type: "int", nullable: false),
                    CommitteeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffCommittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffCommittees_Committees_CommitteeID",
                        column: x => x.CommitteeID,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffCommittees_STAFF_StaffID",
                        column: x => x.StaffID,
                        principalTable: "STAFF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentsCommittees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    CommitteeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsCommittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsCommittees_Committees_CommitteeID",
                        column: x => x.CommitteeID,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsCommittees_STUDENTS_StudentID",
                        column: x => x.StudentID,
                        principalTable: "STUDENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffCommittees_CommitteeID",
                table: "StaffCommittees",
                column: "CommitteeID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffCommittees_StaffID",
                table: "StaffCommittees",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCommittees_CommitteeID",
                table: "StudentsCommittees",
                column: "CommitteeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCommittees_StudentID",
                table: "StudentsCommittees",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffCommittees");

            migrationBuilder.DropTable(
                name: "StudentsCommittees");

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
    }
}
