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

    public virtual DbSet<Assess> Assesses { get; set; }

    public virtual DbSet<Bylaw> Bylaws { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FacultyHieryical> FacultyHieryicals { get; set; }

    public virtual DbSet<FacultyNode> FacultyNodes { get; set; }

    public virtual DbSet<FacultyPhase> FacultyPhases { get; set; }

    public virtual DbSet<FacultySemester> FacultySemesters { get; set; }

    public virtual DbSet<FacultyType> FacultyTypes { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentSemester> StudentSemesters { get; set; }

    public virtual DbSet<StudeyMethod> StudeyMethods { get; set; }

    public virtual DbSet<StuentSatut> StuentSatuts { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubjectAssess> SubjectAssesses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=UNVCenteralDataBase; Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcadYead>(entity =>
        {
            entity.ToTable("ACAD_YEAD");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Assess>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ASSESS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Bylaw>(entity =>
        {
            entity.ToTable("BYLAW");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CodeStudyMethodId).HasColumnName("CODE_STUDY_METHOD_ID");
            entity.Property(e => e.FacultyId).HasColumnName("FACULTY_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");

            entity.HasOne(d => d.CodeStudyMethod).WithMany(p => p.Bylaws)
                .HasForeignKey(d => d.CodeStudyMethodId)
                .HasConstraintName("FK_BYLAW_STUDEY_METHOD");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Bylaws)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK_BYLAW_FACULTY");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.ToTable("FACULTY");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FacultyAddress)
                .HasMaxLength(100)
                .HasColumnName("FACULTY_ADDRESS");
            entity.Property(e => e.FacultyCode)
                .HasMaxLength(100)
                .HasColumnName("FACULTY_CODE");
            entity.Property(e => e.FacultyName)
                .HasMaxLength(100)
                .HasColumnName("FACULTY_NAME");
            entity.Property(e => e.FacultyTypeId).HasColumnName("FACULTY_TYPE_ID");

            entity.HasOne(d => d.FacultyType).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.FacultyTypeId)
                .HasConstraintName("FK_FACULTY_FACULTY_TYPE");
        });

        modelBuilder.Entity<FacultyHieryical>(entity =>
        {
            entity.ToTable("FACULTY_HIERYICAL");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BylawId).HasColumnName("BYLAW_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Order).HasColumnName("ORDER");
            entity.Property(e => e.ParentId).HasColumnName("PARENT_ID");
            entity.Property(e => e.PhaseId).HasColumnName("PHASE_ID");
            entity.Property(e => e.SemeterId).HasColumnName("SEMETER_ID");

            entity.HasOne(d => d.Bylaw).WithMany(p => p.FacultyHieryicals)
                .HasForeignKey(d => d.BylawId)
                .HasConstraintName("FK_FACULTY_HIERYICAL_BYLAW");

            entity.HasOne(d => d.Phase).WithMany(p => p.FacultyHieryicals)
                .HasForeignKey(d => d.PhaseId)
                .HasConstraintName("FK_FACULTY_HIERYICAL_FACULTY_PHASES");

            entity.HasOne(d => d.Semeter).WithMany(p => p.FacultyHieryicals)
                .HasForeignKey(d => d.SemeterId)
                .HasConstraintName("FK_FACULTY_HIERYICAL_FACULTY_SEMESTER");
        });

        modelBuilder.Entity<FacultyNode>(entity =>
        {
            entity.ToTable("FACULTY__NODES");

            entity.Property(e => e.FacultyNodeId)
                .ValueGeneratedNever()
                .HasColumnName("FACULTY_NODE_ID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("CODE");
            entity.Property(e => e.FacultyId).HasColumnName("FACULTY_ID");
            entity.Property(e => e.Level).HasColumnName("LEVEL");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Order).HasColumnName("ORDER");
            entity.Property(e => e.ParentId).HasColumnName("PARENT_ID");

            entity.HasOne(d => d.Faculty).WithMany(p => p.FacultyNodes)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK_FACULTY__NODES_FACULTY");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_FACULTY__NODES_FACULTY__NODES");
        });

        modelBuilder.Entity<FacultyPhase>(entity =>
        {
            entity.ToTable("FACULTY_PHASES");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("CODE");
            entity.Property(e => e.FacultyId).HasColumnName("FACULTY_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Order).HasColumnName("ORDER");

            entity.HasOne(d => d.Faculty).WithMany(p => p.FacultyPhases)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK_FACULTY_PHASES_FACULTY");
        });

        modelBuilder.Entity<FacultySemester>(entity =>
        {
            entity.ToTable("FACULTY_SEMESTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("CODE");
            entity.Property(e => e.FacultyId).HasColumnName("FACULTY_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Order).HasColumnName("ORDER");

            entity.HasOne(d => d.Faculty).WithMany(p => p.FacultySemesters)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK_FACULTY_SEMESTER_FACULTY");
        });

        modelBuilder.Entity<FacultyType>(entity =>
        {
            entity.ToTable("FACULTY_TYPE");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("STUDENTS");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Cityid).HasColumnName("CITYID");
            entity.Property(e => e.Dateofbrith).HasColumnName("DATEOFBRITH");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Facultyid).HasColumnName("FACULTYID");
            entity.Property(e => e.Gender).HasColumnName("GENDER");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .HasColumnName("MOBILE");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Nationalid)
                .HasMaxLength(50)
                .HasColumnName("NATIONALID");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Students)
                .HasForeignKey(d => d.Facultyid)
                .HasConstraintName("FK_STUDENTS_FACULTY");
        });

        modelBuilder.Entity<StudentSemester>(entity =>
        {
            entity.ToTable("STUDENT_SEMESTERS");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AcadYearId).HasColumnName("ACAD_YEAR_ID");
            entity.Property(e => e.FacultyHieryicalId).HasColumnName("FACULTY_HIERYICAL_ID");
            entity.Property(e => e.FacultyNodeId).HasColumnName("FACULTY_NODE_ID");
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(5, 3)")
                .HasColumnName("GPA");
            entity.Property(e => e.IsPass).HasColumnName("IS_PASS");
            entity.Property(e => e.Precentage)
                .HasColumnType("decimal(5, 3)")
                .HasColumnName("PRECENTAGE");
            entity.Property(e => e.StuentId).HasColumnName("STUENT_ID");
            entity.Property(e => e.StuentSatutsId).HasColumnName("STUENT_SATUTS_ID");
            entity.Property(e => e.Toal)
                .HasColumnType("decimal(8, 3)")
                .HasColumnName("TOAL");

            entity.HasOne(d => d.AcadYear).WithMany(p => p.StudentSemesters)
                .HasForeignKey(d => d.AcadYearId)
                .HasConstraintName("FK_STUDENT_SEMESTERS_ACAD_YEAD");

            entity.HasOne(d => d.FacultyHieryical).WithMany(p => p.StudentSemesters)
                .HasForeignKey(d => d.FacultyHieryicalId)
                .HasConstraintName("FK_STUDENT_SEMESTERS_STUDENT_SEMESTERS");

            entity.HasOne(d => d.FacultyNode).WithMany(p => p.StudentSemesters)
                .HasForeignKey(d => d.FacultyNodeId)
                .HasConstraintName("FK_STUDENT_SEMESTERS_FACULTY__NODES");

            entity.HasOne(d => d.Stuent).WithMany(p => p.StudentSemesters)
                .HasForeignKey(d => d.StuentId)
                .HasConstraintName("FK_STUDENT_SEMESTERS_STUDENTS");
        });

        modelBuilder.Entity<StudeyMethod>(entity =>
        {
            entity.ToTable("STUDEY_METHOD");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<StuentSatut>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("STUENT_SATUTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.StuentSatuts)
                .HasMaxLength(50)
                .HasColumnName("STUENT_SATUTS");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.ToTable("SUBJECTS");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreditHours).HasColumnName("CREDIT_HOURS");
            entity.Property(e => e.FacultyHieryicalId).HasColumnName("FACULTY_HIERYICAL_ID");
            entity.Property(e => e.FacultyPhasesId).HasColumnName("FACULTY_PHASES_ID");
            entity.Property(e => e.FacultySemesterId).HasColumnName("FACULTY_SEMESTER_ID");
            entity.Property(e => e.MaxDegree).HasColumnName("MAX_DEGREE");
            entity.Property(e => e.MinDegree).HasColumnName("MIN_DEGREE");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");

            entity.HasOne(d => d.FacultyPhases).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.FacultyPhasesId)
                .HasConstraintName("FK_SUBJECTS_FACULTY_PHASES");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Subject)
                .HasForeignKey<Subject>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SUBJECTS_FACULTY_SEMESTER");
        });

        modelBuilder.Entity<SubjectAssess>(entity =>
        {
            entity.ToTable("SUBJECT_ASSESS");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.MaxDegree).HasColumnName("MAX_DEGREE");
            entity.Property(e => e.MinDegree).HasColumnName("MIN_DEGREE");
            entity.Property(e => e.SubjectId).HasColumnName("SUBJECT_ID");

            entity.HasOne(d => d.Subject).WithMany(p => p.SubjectAssesses)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_SUBJECT_ASSESS_SUBJECTS");
        });
        
         base.OnModelCreating(modelBuilder);
         OnModelCreatingPartial(modelBuilder);
    }
    

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
