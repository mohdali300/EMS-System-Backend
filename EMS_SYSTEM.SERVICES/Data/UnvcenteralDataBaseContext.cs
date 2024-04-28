using System;
using System.Collections.Generic;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM.APPLICATION;

public partial class UnvcenteralDataBaseContext : IdentityDbContext<ApplicationUser>
{
    public UnvcenteralDataBaseContext()
    {
    }

    public UnvcenteralDataBaseContext(DbContextOptions<UnvcenteralDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcadYead> AcadYeads { get; set; }
    public virtual DbSet<ApplicationUser> Users { get; set; }

    public virtual DbSet<Assess> Assesses { get; set; }

    public virtual DbSet<Bylaw> Bylaws { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FacultyHieryical> FacultyHieryicals { get; set; }

    public virtual DbSet<FacultyNode> FacultyNodes { get; set; }

    public virtual DbSet<FacultyPhase> FacultyPhases { get; set; }

    public virtual DbSet<FacultySemester> FacultySemesters { get; set; }

    public virtual DbSet<FacultyType> FacultyTypes { get; set; }

    public virtual DbSet<Palce> Palces { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentSemester> StudentSemesters { get; set; }

    public virtual DbSet<StudentSemesterSubject> StudentSemesterSubjects { get; set; }

    public virtual DbSet<StudeyMethod> StudeyMethods { get; set; }

    public virtual DbSet<StuentSatut> StuentSatuts { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubjectAssess> SubjectAssesses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=UNVCenteralDataBase; Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AcadYead>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Assess>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Bylaw>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.CodeStudyMethod).WithMany(p => p.Bylaws).HasConstraintName("FK_BYLAW_STUDEY_METHOD");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Bylaws).HasConstraintName("FK_BYLAW_FACULTY");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.FacultyType).WithMany(p => p.Faculties).HasConstraintName("FK_FACULTY_FACULTY_TYPE");
        });

        modelBuilder.Entity<FacultyHieryical>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Bylaw).WithMany(p => p.FacultyHieryicals).HasConstraintName("FK_FACULTY_HIERYICAL_BYLAW");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_FACULTY_HIERYICAL_FACULTY_HIERYICAL");

            entity.HasOne(d => d.Phase).WithMany(p => p.FacultyHieryicals).HasConstraintName("FK_FACULTY_HIERYICAL_FACULTY_PHASES");

            entity.HasOne(d => d.Semeter).WithMany(p => p.FacultyHieryicals).HasConstraintName("FK_FACULTY_HIERYICAL_FACULTY_SEMESTER");
        });

        modelBuilder.Entity<FacultyNode>(entity =>
        {
            entity.Property(e => e.FacultyNodeId).ValueGeneratedNever();

            entity.HasOne(d => d.Faculty).WithMany(p => p.FacultyNodes).HasConstraintName("FK_FACULTY__NODES_FACULTY");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_FACULTY__NODES_FACULTY__NODES");
        });

        modelBuilder.Entity<FacultyPhase>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Faculty).WithMany(p => p.FacultyPhases).HasConstraintName("FK_FACULTY_PHASES_FACULTY");
        });

        modelBuilder.Entity<FacultySemester>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Faculty).WithMany(p => p.FacultySemesters).HasConstraintName("FK_FACULTY_SEMESTER_FACULTY");
        });

        modelBuilder.Entity<FacultyType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Palce>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasOne(d => d.Faculty).WithMany().HasConstraintName("FK_STAFF_FACULTY");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Faculty).WithMany(p => p.Students).HasConstraintName("FK_STUDENTS_FACULTY");
        });

        modelBuilder.Entity<StudentSemester>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.AcadYear).WithMany(p => p.StudentSemesters).HasConstraintName("FK_STUDENT_SEMESTERS_ACAD_YEAD");

            entity.HasOne(d => d.FacultyHieryical).WithMany(p => p.StudentSemesters).HasConstraintName("FK_STUDENT_SEMESTERS_STUDENT_SEMESTERS");

            entity.HasOne(d => d.FacultyNode).WithMany(p => p.StudentSemesters).HasConstraintName("FK_STUDENT_SEMESTERS_FACULTY__NODES");

            entity.HasOne(d => d.Stuent).WithMany(p => p.StudentSemesters).HasConstraintName("FK_STUDENT_SEMESTERS_STUDENTS");

            entity.HasOne(d => d.StuentSatuts).WithMany(p => p.StudentSemesters).HasConstraintName("FK_STUDENT_SEMESTERS_STUENT_SATUTS");
        });

        modelBuilder.Entity<StudentSemesterSubject>(entity =>
        {
            entity.Property(e => e.StudentSubjectSemterId).ValueGeneratedNever();

            entity.HasOne(d => d.StudentSemesters).WithMany(p => p.StudentSemesterSubjects).HasConstraintName("FK_STUDENT_SEMESTER_SUBJECT_STUDENT_SEMESTERS");

            entity.HasOne(d => d.Subject).WithMany(p => p.StudentSemesterSubjects).HasConstraintName("FK_STUDENT_SEMESTER_SUBJECT_SUBJECTS");
        });

        modelBuilder.Entity<StudeyMethod>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<StuentSatut>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Subject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SUBJECTS_FACULTY_SEMESTER");
        });

        modelBuilder.Entity<SubjectAssess>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Assess).WithMany(p => p.SubjectAssesses).HasConstraintName("FK_SUBJECT_ASSESS_ASSESS");

            entity.HasOne(d => d.Subject).WithMany(p => p.SubjectAssesses).HasConstraintName("FK_SUBJECT_ASSESS_SUBJECTS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
