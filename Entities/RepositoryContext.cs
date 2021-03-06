using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace Entities
{
    public partial class RepositoryContext :  IdentityDbContext<ApplicationUser>
    {
        public RepositoryContext()
        {
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> AspNetUsers { get; set; }


        public virtual DbSet<AccountsReportChart> AccountsReportCharts { get; set; }
        public virtual DbSet<AccountsReportChartType> AccountsReportChartTypes { get; set; }
        public virtual DbSet<Calculation> Calculations { get; set; }
        public virtual DbSet<Coefficient> Coefficients { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeComponent> EmployeeComponents { get; set; }
        public virtual DbSet<EmployeeGraceType> EmployeeGraceTypes { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<PaymentDaysType> PaymentDaysTypes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<SchemeType> SchemeTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=AZENAISHVILI1;database=Payroll;Trusted_Connection=True;User ID=PayrollModule;Password=NewPass1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //admiadmiadnamiadnadmaidnaadmiadadminadminadmin
            string ADMIN_ID = "772848a0-3b20-46bd-8fd3-d83d110aecf0";
            string ROLE_ID = "036f0246-b844-4471-b450-4c4eef779302";

            //seed admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName ="Admin",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            //create user
            var appUser = new ApplicationUser
            {
                Id = ADMIN_ID,
                Email = "aa@aa.ge",
                EmailConfirmed = true,
                FirstName = "avtandil",
                LastName = "zenai",
                UserName ="26001037",
            };

            //set user password
            //PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            //appUser.PasswordHash = ph.HashPassword(appUser, "NewPass8*");

            //seed user
            modelBuilder.Entity<ApplicationUser>().HasData(appUser);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
            ///////////////////////////////////////////////////
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

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
                    .HasConstraintName("FK__AccountsR__Accou__32AB8735");
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

            modelBuilder.Entity<Calculation>(entity =>
            {
                entity.ToTable("Calculation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CalculationDate).HasColumnType("datetime");

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Gross).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IncomeTax).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Net).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Paid).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PensionTax).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RemainingGraceAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ResId).HasColumnName("Res_id");

                entity.HasOne(d => d.EmployeeComponent)
                    .WithMany(p => p.Calculations)
                    .HasForeignKey(d => d.EmployeeComponentId)
                    .HasConstraintName("FK__Calculati__Emplo__634EBE90");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Calculations)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Calculati__Emplo__625A9A57");

                entity.HasOne(d => d.SchemeType)
                    .WithMany(p => p.Calculations)
                    .HasForeignKey(d => d.SchemeTypeId)
                    .HasConstraintName("FK__Calculati__Schem__6442E2C9");
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

            modelBuilder.Entity<Component>(entity =>
            {
                entity.ToTable("Component");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Coefficient)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.CoefficientId)
                    .HasConstraintName("FK__Component__Coeff__37703C52");

                entity.HasOne(d => d.CreditAccount)
                    .WithMany(p => p.ComponentCreditAccounts)
                    .HasForeignKey(d => d.CreditAccountId)
                    .HasConstraintName("FK__Component__Credi__3587F3E0");

                entity.HasOne(d => d.DebitAccount)
                    .WithMany(p => p.ComponentDebitAccounts)
                    .HasForeignKey(d => d.DebitAccountId)
                    .HasConstraintName("FK__Component__Debit__367C1819");
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

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.BankAccountNumber)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.GraceAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LandIso)
                    .HasMaxLength(10)
                    .HasColumnName("land_iso");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MobilePhone).HasMaxLength(255);

                entity.Property(e => e.PersonalNumber).HasMaxLength(255);

                entity.Property(e => e.Position).HasMaxLength(100);

                entity.Property(e => e.RemainingGraceAmount).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__Employee__Depart__3E1D39E1");

                entity.HasOne(d => d.EmployeeGraceType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeGraceTypeId)
                    .HasConstraintName("FK__Employee__Employ__4C6B5938");

                entity.HasOne(d => d.EmployeeType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .HasConstraintName("FK__Employee__Employ__498EEC8D");

                entity.HasOne(d => d.SchemeType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.SchemeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Scheme__3F115E1A");
            });

            modelBuilder.Entity<EmployeeComponent>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CashAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.EmployeeComponents)
                    .HasForeignKey(d => d.ComponentId)
                    .HasConstraintName("FK__EmployeeC__Compo__41EDCAC5");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.EmployeeComponents)
                    .HasForeignKey(d => d.CostCenterId)
                    .HasConstraintName("FK__EmployeeC__CostC__42E1EEFE");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeComponents)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__EmployeeC__Emplo__43D61337");

                entity.HasOne(d => d.PaymentDaysType)
                    .WithMany(p => p.EmployeeComponents)
                    .HasForeignKey(d => d.PaymentDaysTypeId)
                    .HasConstraintName("FK__EmployeeC__Payme__46B27FE2");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.EmployeeComponents)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__EmployeeC__Proje__44CA3770");

                entity.HasOne(d => d.SchemeType)
                    .WithMany(p => p.EmployeeComponents)
                    .HasForeignKey(d => d.SchemeTypeId)
                    .HasConstraintName("FK__EmployeeC__Schem__45BE5BA9");
            });

            modelBuilder.Entity<EmployeeGraceType>(entity =>
            {
                entity.ToTable("EmployeeGraceType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<EmployeeType>(entity =>
            {
                entity.ToTable("EmployeeType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PaymentDaysType>(entity =>
            {
                entity.ToTable("PaymentDaysType");

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

            modelBuilder.Entity<SchemeType>(entity =>
            {
                entity.ToTable("SchemeType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
