using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SessionTwoPartTWo.DataBaseFolder;

public partial class User04Context : DbContext
{
    public User04Context()
    {
    }

    public User04Context(DbContextOptions<User04Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Hospitalization> Hospitalizations { get; set; }

    public virtual DbSet<InsuranceCompany> InsuranceCompanies { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<TerapeuticDiagnostic> TerapeuticDiagnostics { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=192.168.200.35;user=user04;password=93499;database=user04;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Condition>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctor");

            entity.Property(e => e.FiratName).HasMaxLength(250);
            entity.Property(e => e.LastName).HasMaxLength(250);
            entity.Property(e => e.Patronymic).HasMaxLength(250);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Hospitalization>(entity =>
        {
            entity.ToTable("Hospitalization");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.AdditionInformation).HasColumnType("text");
            entity.Property(e => e.BedNumber).HasMaxLength(100);
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Diagnoz).HasColumnType("text");
            entity.Property(e => e.Length).HasMaxLength(250);
            entity.Property(e => e.Purpose).HasMaxLength(250);
            entity.Property(e => e.RoomNumber).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Conditions).WithMany(p => p.Hospitalizations)
                .HasForeignKey(d => d.ConditionsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hospitalization_Conditions");

            entity.HasOne(d => d.Department).WithMany(p => p.Hospitalizations)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Hospitalization_Department");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Hospitalization)
                .HasForeignKey<Hospitalization>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hospitalization_Patient");
        });

        modelBuilder.Entity<InsuranceCompany>(entity =>
        {
            entity.ToTable("InsuranceCompany");

            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.MedicCardCode);

            entity.ToTable("Patient");

            entity.Property(e => e.MedicCardCode).ValueGeneratedNever();
            entity.Property(e => e.Adress).HasMaxLength(250);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.DateGetMedicCard).HasColumnType("date");
            entity.Property(e => e.DateLastVisit).HasColumnType("date");
            entity.Property(e => e.DateNextVisit).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(250);
            entity.Property(e => e.LastName).HasMaxLength(250);
            entity.Property(e => e.MedicHistory).HasColumnType("text");
            entity.Property(e => e.PasportNumber)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.PasportSeries)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.Patronymic).HasMaxLength(250);
            entity.Property(e => e.PhoneNumber).HasMaxLength(17);
            entity.Property(e => e.Photo).HasColumnType("image");
            entity.Property(e => e.StopDateInsuransyPolice).HasColumnType("date");
            entity.Property(e => e.WorkPlase).HasMaxLength(250);

            entity.HasOne(d => d.Gender).WithMany(p => p.Patients)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Gender");

            entity.HasOne(d => d.InsuranceCompany).WithMany(p => p.Patients)
                .HasForeignKey(d => d.InsuranceCompanyId)
                .HasConstraintName("FK_Patient_InsuranceCompany");
        });

        modelBuilder.Entity<TerapeuticDiagnostic>(entity =>
        {
            entity.ToTable("TerapeuticDiagnostic");

            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Recommendations).HasColumnType("text");
            entity.Property(e => e.Result).HasColumnType("text");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Doctor).WithMany(p => p.TerapeuticDiagnostics)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TerapeuticDiagnostic_Doctor");

            entity.HasOne(d => d.MedicCardCodeNavigation).WithMany(p => p.TerapeuticDiagnostics)
                .HasForeignKey(d => d.MedicCardCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TerapeuticDiagnostic_Patient");

            entity.HasOne(d => d.Type).WithMany(p => p.TerapeuticDiagnostics)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_TerapeuticDiagnostic_Type");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.ToTable("Type");

            entity.Property(e => e.Name).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
