using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS_SYSTEM.APPLICATION.Migrations
{
    /// <inheritdoc />
    public partial class adduserstabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACAD_YEAD",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACAD_YEAD", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ASSESS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASSESS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FACULTY_TYPE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACULTY_TYPE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PALCES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CAPACITY = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PALCES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "STUDEY_METHOD",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDEY_METHOD", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "STUENT_SATUTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    STUENT_SATUTS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUENT_SATUTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FACULTY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    FACULTY_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FACULTY_CODE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FACULTY_ADDRESS = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FACULTY_TYPE_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACULTY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FACULTY_FACULTY_TYPE",
                        column: x => x.FACULTY_TYPE_ID,
                        principalTable: "FACULTY_TYPE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "BYLAW",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FACULTY_ID = table.Column<int>(type: "int", nullable: true),
                    CODE_STUDY_METHOD_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BYLAW", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BYLAW_FACULTY",
                        column: x => x.FACULTY_ID,
                        principalTable: "FACULTY",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_BYLAW_STUDEY_METHOD",
                        column: x => x.CODE_STUDY_METHOD_ID,
                        principalTable: "STUDEY_METHOD",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FACULTY__NODES",
                columns: table => new
                {
                    FACULTY_NODE_ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ORDER = table.Column<int>(type: "int", nullable: true),
                    LEVEL = table.Column<int>(type: "int", nullable: true),
                    FACULTY_ID = table.Column<int>(type: "int", nullable: true),
                    PARENT_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACULTY__NODES", x => x.FACULTY_NODE_ID);
                    table.ForeignKey(
                        name: "FK_FACULTY__NODES_FACULTY",
                        column: x => x.FACULTY_ID,
                        principalTable: "FACULTY",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FACULTY__NODES_FACULTY__NODES",
                        column: x => x.PARENT_ID,
                        principalTable: "FACULTY__NODES",
                        principalColumn: "FACULTY_NODE_ID");
                });

            migrationBuilder.CreateTable(
                name: "FACULTY_PHASES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FACULTY_ID = table.Column<int>(type: "int", nullable: true),
                    ORDER = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACULTY_PHASES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FACULTY_PHASES_FACULTY",
                        column: x => x.FACULTY_ID,
                        principalTable: "FACULTY",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FACULTY_SEMESTER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FACULTY_ID = table.Column<int>(type: "int", nullable: true),
                    ORDER = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACULTY_SEMESTER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FACULTY_SEMESTER_FACULTY",
                        column: x => x.FACULTY_ID,
                        principalTable: "FACULTY",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "STAFF",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FACULTY_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_STAFF_FACULTY",
                        column: x => x.FACULTY_ID,
                        principalTable: "FACULTY",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "STUDENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CITYID = table.Column<int>(type: "int", nullable: true),
                    NATIONALID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FACULTYCODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GENDER = table.Column<int>(type: "int", nullable: true),
                    DATEOFBRITH = table.Column<DateOnly>(type: "date", nullable: true),
                    FACULTYID = table.Column<int>(type: "int", nullable: true),
                    MOBILE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STUDENTS_FACULTY",
                        column: x => x.FACULTYID,
                        principalTable: "FACULTY",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FACULTY_HIERYICAL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ORDER = table.Column<int>(type: "int", nullable: true),
                    PARENT_ID = table.Column<int>(type: "int", nullable: true),
                    BYLAW_ID = table.Column<int>(type: "int", nullable: true),
                    SEMETER_ID = table.Column<int>(type: "int", nullable: true),
                    PHASE_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACULTY_HIERYICAL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FACULTY_HIERYICAL_BYLAW",
                        column: x => x.BYLAW_ID,
                        principalTable: "BYLAW",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FACULTY_HIERYICAL_FACULTY_HIERYICAL",
                        column: x => x.PARENT_ID,
                        principalTable: "FACULTY_HIERYICAL",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FACULTY_HIERYICAL_FACULTY_PHASES",
                        column: x => x.PHASE_ID,
                        principalTable: "FACULTY_PHASES",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FACULTY_HIERYICAL_FACULTY_SEMESTER",
                        column: x => x.SEMETER_ID,
                        principalTable: "FACULTY_SEMESTER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SUBJECTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FACULTY_HIERYICAL_ID = table.Column<int>(type: "int", nullable: true),
                    MAX_DEGREE = table.Column<int>(type: "int", nullable: true),
                    MIN_DEGREE = table.Column<int>(type: "int", nullable: true),
                    CREDIT_HOURS = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUBJECTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SUBJECTS_FACULTY_SEMESTER",
                        column: x => x.ID,
                        principalTable: "FACULTY_SEMESTER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "STUDENT_SEMESTERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    STUENT_ID = table.Column<int>(type: "int", nullable: true),
                    FACULTY_HIERYICAL_ID = table.Column<int>(type: "int", nullable: true),
                    ACAD_YEAR_ID = table.Column<int>(type: "int", nullable: true),
                    GPA = table.Column<decimal>(type: "decimal(5,3)", nullable: true),
                    PRECENTAGE = table.Column<decimal>(type: "decimal(5,3)", nullable: true),
                    TOAL = table.Column<decimal>(type: "decimal(8,3)", nullable: true),
                    IS_PASS = table.Column<int>(type: "int", nullable: true),
                    STUENT_SATUTS_ID = table.Column<int>(type: "int", nullable: true),
                    FACULTY_NODE_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENT_SEMESTERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STUDENT_SEMESTERS_ACAD_YEAD",
                        column: x => x.ACAD_YEAR_ID,
                        principalTable: "ACAD_YEAD",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_STUDENT_SEMESTERS_FACULTY__NODES",
                        column: x => x.FACULTY_NODE_ID,
                        principalTable: "FACULTY__NODES",
                        principalColumn: "FACULTY_NODE_ID");
                    table.ForeignKey(
                        name: "FK_STUDENT_SEMESTERS_STUDENTS",
                        column: x => x.STUENT_ID,
                        principalTable: "STUDENTS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_STUDENT_SEMESTERS_STUDENT_SEMESTERS",
                        column: x => x.FACULTY_HIERYICAL_ID,
                        principalTable: "FACULTY_HIERYICAL",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_STUDENT_SEMESTERS_STUENT_SATUTS",
                        column: x => x.STUENT_SATUTS_ID,
                        principalTable: "STUENT_SATUTS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SUBJECT_ASSESS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    MAX_DEGREE = table.Column<int>(type: "int", nullable: true),
                    MIN_DEGREE = table.Column<int>(type: "int", nullable: true),
                    SUBJECT_ID = table.Column<int>(type: "int", nullable: true),
                    ASSESS_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUBJECT_ASSESS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SUBJECT_ASSESS_ASSESS",
                        column: x => x.ASSESS_ID,
                        principalTable: "ASSESS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SUBJECT_ASSESS_SUBJECTS",
                        column: x => x.SUBJECT_ID,
                        principalTable: "SUBJECTS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "STUDENT_SEMESTER_SUBJECT",
                columns: table => new
                {
                    Student_Subject_Semter_Id = table.Column<int>(type: "int", nullable: false),
                    Subject_id = table.Column<int>(type: "int", nullable: true),
                    Degree = table.Column<decimal>(type: "decimal(8,3)", nullable: true),
                    Is_Passed = table.Column<int>(type: "int", nullable: true),
                    STUDENT_SEMESTERS_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENT_SEMESTER_SUBJECT", x => x.Student_Subject_Semter_Id);
                    table.ForeignKey(
                        name: "FK_STUDENT_SEMESTER_SUBJECT_STUDENT_SEMESTERS",
                        column: x => x.STUDENT_SEMESTERS_Id,
                        principalTable: "STUDENT_SEMESTERS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_STUDENT_SEMESTER_SUBJECT_SUBJECTS",
                        column: x => x.Subject_id,
                        principalTable: "SUBJECTS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BYLAW_CODE_STUDY_METHOD_ID",
                table: "BYLAW",
                column: "CODE_STUDY_METHOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BYLAW_FACULTY_ID",
                table: "BYLAW",
                column: "FACULTY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FACULTY_FACULTY_TYPE_ID",
                table: "FACULTY",
                column: "FACULTY_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FACULTY__NODES_FACULTY_ID",
                table: "FACULTY__NODES",
                column: "FACULTY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FACULTY__NODES_PARENT_ID",
                table: "FACULTY__NODES",
                column: "PARENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FACULTY_HIERYICAL_BYLAW_ID",
                table: "FACULTY_HIERYICAL",
                column: "BYLAW_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FACULTY_HIERYICAL_PARENT_ID",
                table: "FACULTY_HIERYICAL",
                column: "PARENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FACULTY_HIERYICAL_PHASE_ID",
                table: "FACULTY_HIERYICAL",
                column: "PHASE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FACULTY_HIERYICAL_SEMETER_ID",
                table: "FACULTY_HIERYICAL",
                column: "SEMETER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FACULTY_PHASES_FACULTY_ID",
                table: "FACULTY_PHASES",
                column: "FACULTY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FACULTY_SEMESTER_FACULTY_ID",
                table: "FACULTY_SEMESTER",
                column: "FACULTY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_FACULTY_ID",
                table: "STAFF",
                column: "FACULTY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_SEMESTER_SUBJECT_STUDENT_SEMESTERS_Id",
                table: "STUDENT_SEMESTER_SUBJECT",
                column: "STUDENT_SEMESTERS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_SEMESTER_SUBJECT_Subject_id",
                table: "STUDENT_SEMESTER_SUBJECT",
                column: "Subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_SEMESTERS_ACAD_YEAR_ID",
                table: "STUDENT_SEMESTERS",
                column: "ACAD_YEAR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_SEMESTERS_FACULTY_HIERYICAL_ID",
                table: "STUDENT_SEMESTERS",
                column: "FACULTY_HIERYICAL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_SEMESTERS_FACULTY_NODE_ID",
                table: "STUDENT_SEMESTERS",
                column: "FACULTY_NODE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_SEMESTERS_STUENT_ID",
                table: "STUDENT_SEMESTERS",
                column: "STUENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_SEMESTERS_STUENT_SATUTS_ID",
                table: "STUDENT_SEMESTERS",
                column: "STUENT_SATUTS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENTS_FACULTYID",
                table: "STUDENTS",
                column: "FACULTYID");

            migrationBuilder.CreateIndex(
                name: "IX_SUBJECT_ASSESS_ASSESS_ID",
                table: "SUBJECT_ASSESS",
                column: "ASSESS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SUBJECT_ASSESS_SUBJECT_ID",
                table: "SUBJECT_ASSESS",
                column: "SUBJECT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PALCES");

            migrationBuilder.DropTable(
                name: "STAFF");

            migrationBuilder.DropTable(
                name: "STUDENT_SEMESTER_SUBJECT");

            migrationBuilder.DropTable(
                name: "SUBJECT_ASSESS");

            migrationBuilder.DropTable(
                name: "STUDENT_SEMESTERS");

            migrationBuilder.DropTable(
                name: "ASSESS");

            migrationBuilder.DropTable(
                name: "SUBJECTS");

            migrationBuilder.DropTable(
                name: "ACAD_YEAD");

            migrationBuilder.DropTable(
                name: "FACULTY__NODES");

            migrationBuilder.DropTable(
                name: "STUDENTS");

            migrationBuilder.DropTable(
                name: "FACULTY_HIERYICAL");

            migrationBuilder.DropTable(
                name: "STUENT_SATUTS");

            migrationBuilder.DropTable(
                name: "BYLAW");

            migrationBuilder.DropTable(
                name: "FACULTY_PHASES");

            migrationBuilder.DropTable(
                name: "FACULTY_SEMESTER");

            migrationBuilder.DropTable(
                name: "STUDEY_METHOD");

            migrationBuilder.DropTable(
                name: "FACULTY");

            migrationBuilder.DropTable(
                name: "FACULTY_TYPE");
        }
    }
}
