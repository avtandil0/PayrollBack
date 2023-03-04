using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
#nullable disable

namespace Entities
{
    public partial class RepositoryContext : IdentityDbContext<ApplicationUser>
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
        public virtual DbSet<TimePeriod> TimePeriods { get; set; }
        public virtual DbSet<TimeSheet> TimeSheets { get; set; }
        public virtual DbSet<PayrollReportDatum> PayrollReportData { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=AZENAISHVILI1;database=PayrollNew;Trusted_Connection=True;User ID=PayrollModule;Password=NewPass1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Rate>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("rates");

                entity.Property(e => e.Currency)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("currency");

                entity.Property(e => e.ExchangeRate).HasPrecision(19, 4)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("exchangeRate");

                entity.Property(e => e.Date)
                   .HasColumnType("datetime")
                   .HasColumnName("date");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<AccountsReportChart>(entity =>
            {
                entity.ToTable("AccountsReportChart");

                entity.HasIndex(e => e.AccountsReportChartTypeId, "IX_AccountsReportChart_AccountsReportChartTypeId");

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

                entity.HasIndex(e => e.EmployeeComponentId, "IX_Calculation_EmployeeComponentId");

                entity.HasIndex(e => e.EmployeeId, "IX_Calculation_EmployeeId");

                entity.HasIndex(e => e.SchemeTypeId, "IX_Calculation_SchemeTypeId");

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
                entity.Property(e => e.TotalBalance).HasColumnType("decimal(18, 0)");

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

                entity.HasIndex(e => e.CoefficientId, "IX_Component_CoefficientId");

                entity.HasIndex(e => e.CreditAccountId, "IX_Component_CreditAccountId");

                entity.HasIndex(e => e.DebitAccountId, "IX_Component_DebitAccountId");

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

                entity.HasIndex(e => e.DepartmentId, "IX_Employee_DepartmentId");

                entity.HasIndex(e => e.EmployeeGraceTypeId, "IX_Employee_EmployeeGraceTypeId");

                entity.HasIndex(e => e.EmployeeTypeId, "IX_Employee_EmployeeTypeId");

                entity.HasIndex(e => e.SchemeTypeId, "IX_Employee_SchemeTypeId");

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
                entity.HasIndex(e => e.ComponentId, "IX_EmployeeComponents_ComponentId");

                entity.HasIndex(e => e.CostCenterId, "IX_EmployeeComponents_CostCenterId");

                entity.HasIndex(e => e.EmployeeId, "IX_EmployeeComponents_EmployeeId");

                entity.HasIndex(e => e.PaymentDaysTypeId, "IX_EmployeeComponents_PaymentDaysTypeId");

                entity.HasIndex(e => e.ProjectId, "IX_EmployeeComponents_ProjectId");

                entity.HasIndex(e => e.SchemeTypeId, "IX_EmployeeComponents_SchemeTypeId");

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

                entity.Property(e => e.IsPermanent).HasColumnName("isPermanent");

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

            modelBuilder.Entity<PayrollReportDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("payroll_report_data");

                entity.Property(e => e.Address1)
                    .HasMaxLength(90)
                    .HasColumnName("address1");

                entity.Property(e => e.BaseValue).HasColumnName("base_value");

                entity.Property(e => e.CalculationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("calculationDate");

                entity.Property(e => e.CompCode)
                    .IsRequired()
                    .HasMaxLength(90)
                    .HasColumnName("comp_code");

                entity.Property(e => e.DepartmentId).HasColumnName("departmentID");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(40)
                    .HasColumnName("departmentName");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("firstName");

                entity.Property(e => e.GraceValue).HasColumnName("grace_value");

                entity.Property(e => e.HrcompTransId).HasColumnName("hrcomp_trans_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IncomeTax).HasColumnName("income_tax");

                entity.Property(e => e.InitialGrace).HasColumnName("initial_grace");

                entity.Property(e => e.IssuedAmount).HasColumnName("issued_amount");

                entity.Property(e => e.LandIso)
                    .HasMaxLength(10)
                    .HasColumnName("land_iso")
                    .IsFixedLength(true);

                entity.Property(e => e.LandIsonr)
                    .HasMaxLength(10)
                    .HasColumnName("land_isonr")
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("lastName");

                entity.Property(e => e.Oms600)
                    .HasMaxLength(90)
                    .HasColumnName("oms60_0");

                entity.Property(e => e.PayrollYear).HasColumnName("payrollYear");

                entity.Property(e => e.PensionSchema).HasColumnName("pension_schema");

                entity.Property(e => e.Period).HasColumnName("period");

                entity.Property(e => e.PersonalNumber)
                    .HasMaxLength(11)
                    .HasColumnName("personalNumber")
                    .IsFixedLength(true);

                entity.Property(e => e.RemainingGrace).HasColumnName("remaining_grace");

                entity.Property(e => e.ResId).HasColumnName("res_id");
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

            modelBuilder.Entity<TimePeriod>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.EndTime)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.IsBreakTime).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartTime)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("startTime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimePeriods)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__TimePerio__Emplo__14270015");
            });

            modelBuilder.Entity<TimeSheet>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BreakingEndTime)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.BreakingStartTime)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.DateChange).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.WeekDay)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.WorkingEndTime)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.WorkingStartTime)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
