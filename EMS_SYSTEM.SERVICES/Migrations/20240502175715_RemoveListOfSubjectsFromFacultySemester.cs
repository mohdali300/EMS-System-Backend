using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_SYSTEM.APPLICATION.Migrations
{
    /// <inheritdoc />
    public partial class RemoveListOfSubjectsFromFacultySemester : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SUBJECTS_FACULTY_SEMESTER_FacultySemesterId",
                table: "SUBJECTS");

            migrationBuilder.DropIndex(
                name: "IX_SUBJECTS_FacultySemesterId",
                table: "SUBJECTS");

            migrationBuilder.DropColumn(
                name: "FacultySemesterId",
                table: "SUBJECTS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultySemesterId",
                table: "SUBJECTS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SUBJECTS_FacultySemesterId",
                table: "SUBJECTS",
                column: "FacultySemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_SUBJECTS_FACULTY_SEMESTER_FacultySemesterId",
                table: "SUBJECTS",
                column: "FacultySemesterId",
                principalTable: "FACULTY_SEMESTER",
                principalColumn: "ID");
        }
    }
}
