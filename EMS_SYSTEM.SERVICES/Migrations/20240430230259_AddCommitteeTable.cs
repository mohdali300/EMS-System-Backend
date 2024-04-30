using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_SYSTEM.APPLICATION.Migrations
{
    /// <inheritdoc />
    public partial class AddCommitteeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SUBJECTS_FACULTY_SEMESTER",
                table: "SUBJECTS");

            migrationBuilder.RenameColumn(
                name: "FACULTY_HIERYICAL_ID",
                table: "SUBJECTS",
                newName: "FACULTY_SEMESTER_ID");

            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Interval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    ByLawId = table.Column<int>(type: "int", nullable: false),
                    FacultyNodeId = table.Column<int>(type: "int", nullable: false),
                    FacultyPhaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Committees_BYLAW_ByLawId",
                        column: x => x.ByLawId,
                        principalTable: "BYLAW",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Committees_FACULTY_PHASES_FacultyPhaseId",
                        column: x => x.FacultyPhaseId,
                        principalTable: "FACULTY_PHASES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Committees_FACULTY__NODES_FacultyNodeId",
                        column: x => x.FacultyNodeId,
                        principalTable: "FACULTY__NODES",
                        principalColumn: "FACULTY_NODE_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCommittees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    CommitteeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCommittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectCommittees_Committees_CommitteeId",
                        column: x => x.CommitteeId,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectCommittees_SUBJECTS_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SUBJECTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SUBJECTS_FACULTY_SEMESTER_ID",
                table: "SUBJECTS",
                column: "FACULTY_SEMESTER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_ByLawId",
                table: "Committees",
                column: "ByLawId");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_FacultyNodeId",
                table: "Committees",
                column: "FacultyNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_FacultyPhaseId",
                table: "Committees",
                column: "FacultyPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCommittees_CommitteeId",
                table: "SubjectCommittees",
                column: "CommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCommittees_SubjectId",
                table: "SubjectCommittees",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SUBJECTS_FACULTY_SEMESTER",
                table: "SUBJECTS",
                column: "FACULTY_SEMESTER_ID",
                principalTable: "FACULTY_SEMESTER",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SUBJECTS_FACULTY_SEMESTER",
                table: "SUBJECTS");

            migrationBuilder.DropTable(
                name: "SubjectCommittees");

            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.DropIndex(
                name: "IX_SUBJECTS_FACULTY_SEMESTER_ID",
                table: "SUBJECTS");

            migrationBuilder.RenameColumn(
                name: "FACULTY_SEMESTER_ID",
                table: "SUBJECTS",
                newName: "FACULTY_HIERYICAL_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SUBJECTS_FACULTY_SEMESTER",
                table: "SUBJECTS",
                column: "ID",
                principalTable: "FACULTY_SEMESTER",
                principalColumn: "ID");
        }
    }
}
