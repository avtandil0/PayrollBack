using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Entities.Models;

#nullable disable

namespace Entities
{
    public partial class RepositoryContext : DbContext
    {
        public RepositoryContext()
        {
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountsReportChart> AccountsReportCharts { get; set; }
        public virtual DbSet<AccountsReportChartType> AccountsReportChartTypes { get; set; }
        public virtual DbSet<Coefficient> Coefficients { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;database=Payroll;Trusted_Connection=True;User ID=PayrollModule;Password=NewPass1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AccountsReportChart>(entity =>
            {
                entity.ToTable("AccountsReportChart");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.AccountsReportChartType)
                    .WithMany(p => p.AccountsReportCharts)
                    .HasForeignKey(d => d.AccountsReportChartTypeId)
                    .HasConstraintName("FK__AccountsR__Accou__5441852A");
            });

            modelBuilder.Entity<AccountsReportChartType>(entity =>
            {
                entity.ToTable("AccountsReportChartType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Coefficient>(entity =>
            {
                entity.ToTable("Coefficient");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Pgross).HasColumnName("PGross");

                entity.Property(e => e.PincomeTax).HasColumnName("PIncomeTax");

                entity.Property(e => e.Pnet).HasColumnName("PNet");

                entity.Property(e => e.Ppaid).HasColumnName("PPaid");

                entity.Property(e => e.Ppension).HasColumnName("PPension");

                entity.Property(e => e.Ptax1).HasColumnName("PTax1");

                entity.Property(e => e.Ptax2).HasColumnName("PTax2");

                entity.Property(e => e.Sgross).HasColumnName("SGross");

                entity.Property(e => e.SincomeTax).HasColumnName("SIncomeTax");

                entity.Property(e => e.Snet).HasColumnName("SNet");

                entity.Property(e => e.Spaid).HasColumnName("SPaid");

                entity.Property(e => e.Spension).HasColumnName("SPension");

                entity.Property(e => e.Stax1).HasColumnName("STax1");

                entity.Property(e => e.Stax2).HasColumnName("STax2");
            });

            modelBuilder.Entity<CostCenter>(entity =>
            {
                entity.ToTable("CostCenter");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
