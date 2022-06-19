﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entities.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20220612133215_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.AccountsReportChart", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountsReportChartTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AccountsReportChartTypeId");

                    b.ToTable("AccountsReportChart");
                });

            modelBuilder.Entity("Entities.Models.AccountsReportChartType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("AccountsReportChartType");
                });

            modelBuilder.Entity("Entities.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateChanged")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Entities.Models.Calculation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CalculationDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("EmployeeComponentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Gross")
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("IncomeTax")
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("Net")
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("Paid")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int?>("PayrollMonth")
                        .HasColumnType("int");

                    b.Property<int?>("PayrollYear")
                        .HasColumnType("int");

                    b.Property<decimal>("PensionTax")
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal?>("RemainingGraceAmount")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int?>("ResId")
                        .HasColumnType("int")
                        .HasColumnName("Res_id");

                    b.Property<int?>("SchemeTypeId")
                        .HasColumnType("int");

                    b.Property<double>("Tax1")
                        .HasColumnType("float");

                    b.Property<double>("Tax2")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeComponentId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("SchemeTypeId");

                    b.ToTable("Calculation");
                });

            modelBuilder.Entity("Entities.Models.Coefficient", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double>("Pgross")
                        .HasColumnType("float")
                        .HasColumnName("PGross");

                    b.Property<double>("PincomeTax")
                        .HasColumnType("float")
                        .HasColumnName("PIncomeTax");

                    b.Property<double>("Pnet")
                        .HasColumnType("float")
                        .HasColumnName("PNet");

                    b.Property<double>("Ppaid")
                        .HasColumnType("float")
                        .HasColumnName("PPaid");

                    b.Property<double>("Ppension")
                        .HasColumnType("float")
                        .HasColumnName("PPension");

                    b.Property<double>("Ptax1")
                        .HasColumnType("float")
                        .HasColumnName("PTax1");

                    b.Property<double>("Ptax2")
                        .HasColumnType("float")
                        .HasColumnName("PTax2");

                    b.Property<double>("Sgross")
                        .HasColumnType("float")
                        .HasColumnName("SGross");

                    b.Property<double>("SincomeTax")
                        .HasColumnType("float")
                        .HasColumnName("SIncomeTax");

                    b.Property<double>("Snet")
                        .HasColumnType("float")
                        .HasColumnName("SNet");

                    b.Property<double>("Spaid")
                        .HasColumnType("float")
                        .HasColumnName("SPaid");

                    b.Property<double>("Spension")
                        .HasColumnType("float")
                        .HasColumnName("SPension");

                    b.Property<double>("Stax1")
                        .HasColumnType("float")
                        .HasColumnName("STax1");

                    b.Property<double>("Stax2")
                        .HasColumnType("float")
                        .HasColumnName("STax2");

                    b.HasKey("Id");

                    b.ToTable("Coefficient");
                });

            modelBuilder.Entity("Entities.Models.Component", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CoefficientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreditAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("DebitAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("CoefficientId");

                    b.HasIndex("CreditAccountId");

                    b.HasIndex("DebitAccountId");

                    b.ToTable("Component");
                });

            modelBuilder.Entity("Entities.Models.CostCenter", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("CostCenter");
                });

            modelBuilder.Entity("Entities.Models.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Entities.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("BankAccountNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("EmployeeGraceTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeTypeId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("GraceAmount")
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("LandIso")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("land_iso");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PersonalNumber")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Position")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("RemainingGraceAmount")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int?>("ResId")
                        .HasColumnType("int");

                    b.Property<int>("SchemeTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EmployeeGraceTypeId");

                    b.HasIndex("EmployeeTypeId");

                    b.HasIndex("SchemeTypeId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Entities.Models.EmployeeComponent", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("CashAmount")
                        .HasColumnType("decimal(18,0)");

                    b.Property<Guid?>("ComponentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CostCenterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("PaidByCash")
                        .HasColumnType("bit");

                    b.Property<bool?>("PaidMultiple")
                        .HasColumnType("bit");

                    b.Property<int?>("PaymentDaysTypeId")
                        .HasColumnType("int");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("SchemeTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("CostCenterId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PaymentDaysTypeId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SchemeTypeId");

                    b.ToTable("EmployeeComponents");
                });

            modelBuilder.Entity("Entities.Models.EmployeeGraceType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,0)");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("EmployeeGraceType");
                });

            modelBuilder.Entity("Entities.Models.EmployeeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("EmployeeType");
                });

            modelBuilder.Entity("Entities.Models.PaymentDaysType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("PaymentDaysType");
                });

            modelBuilder.Entity("Entities.Models.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Entities.Models.SchemeType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("SchemeType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Entities.Models.AccountsReportChart", b =>
                {
                    b.HasOne("Entities.Models.AccountsReportChartType", "AccountsReportChartType")
                        .WithMany("AccountsReportCharts")
                        .HasForeignKey("AccountsReportChartTypeId")
                        .HasConstraintName("FK__AccountsR__Accou__32AB8735");

                    b.Navigation("AccountsReportChartType");
                });

            modelBuilder.Entity("Entities.Models.Calculation", b =>
                {
                    b.HasOne("Entities.Models.EmployeeComponent", "EmployeeComponent")
                        .WithMany("Calculations")
                        .HasForeignKey("EmployeeComponentId")
                        .HasConstraintName("FK__Calculati__Emplo__634EBE90");

                    b.HasOne("Entities.Models.Employee", "Employee")
                        .WithMany("Calculations")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK__Calculati__Emplo__625A9A57");

                    b.HasOne("Entities.Models.SchemeType", "SchemeType")
                        .WithMany("Calculations")
                        .HasForeignKey("SchemeTypeId")
                        .HasConstraintName("FK__Calculati__Schem__6442E2C9");

                    b.Navigation("Employee");

                    b.Navigation("EmployeeComponent");

                    b.Navigation("SchemeType");
                });

            modelBuilder.Entity("Entities.Models.Component", b =>
                {
                    b.HasOne("Entities.Models.Coefficient", "Coefficient")
                        .WithMany("Components")
                        .HasForeignKey("CoefficientId")
                        .HasConstraintName("FK__Component__Coeff__37703C52");

                    b.HasOne("Entities.Models.AccountsReportChart", "CreditAccount")
                        .WithMany("ComponentCreditAccounts")
                        .HasForeignKey("CreditAccountId")
                        .HasConstraintName("FK__Component__Credi__3587F3E0");

                    b.HasOne("Entities.Models.AccountsReportChart", "DebitAccount")
                        .WithMany("ComponentDebitAccounts")
                        .HasForeignKey("DebitAccountId")
                        .HasConstraintName("FK__Component__Debit__367C1819");

                    b.Navigation("Coefficient");

                    b.Navigation("CreditAccount");

                    b.Navigation("DebitAccount");
                });

            modelBuilder.Entity("Entities.Models.Employee", b =>
                {
                    b.HasOne("Entities.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK__Employee__Depart__3E1D39E1");

                    b.HasOne("Entities.Models.EmployeeGraceType", "EmployeeGraceType")
                        .WithMany("Employees")
                        .HasForeignKey("EmployeeGraceTypeId")
                        .HasConstraintName("FK__Employee__Employ__4C6B5938");

                    b.HasOne("Entities.Models.EmployeeType", "EmployeeType")
                        .WithMany("Employees")
                        .HasForeignKey("EmployeeTypeId")
                        .HasConstraintName("FK__Employee__Employ__498EEC8D");

                    b.HasOne("Entities.Models.SchemeType", "SchemeType")
                        .WithMany("Employees")
                        .HasForeignKey("SchemeTypeId")
                        .HasConstraintName("FK__Employee__Scheme__3F115E1A")
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("EmployeeGraceType");

                    b.Navigation("EmployeeType");

                    b.Navigation("SchemeType");
                });

            modelBuilder.Entity("Entities.Models.EmployeeComponent", b =>
                {
                    b.HasOne("Entities.Models.Component", "Component")
                        .WithMany("EmployeeComponents")
                        .HasForeignKey("ComponentId")
                        .HasConstraintName("FK__EmployeeC__Compo__41EDCAC5");

                    b.HasOne("Entities.Models.CostCenter", "CostCenter")
                        .WithMany("EmployeeComponents")
                        .HasForeignKey("CostCenterId")
                        .HasConstraintName("FK__EmployeeC__CostC__42E1EEFE");

                    b.HasOne("Entities.Models.Employee", "Employee")
                        .WithMany("EmployeeComponents")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK__EmployeeC__Emplo__43D61337");

                    b.HasOne("Entities.Models.PaymentDaysType", "PaymentDaysType")
                        .WithMany("EmployeeComponents")
                        .HasForeignKey("PaymentDaysTypeId")
                        .HasConstraintName("FK__EmployeeC__Payme__46B27FE2");

                    b.HasOne("Entities.Models.Project", "Project")
                        .WithMany("EmployeeComponents")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK__EmployeeC__Proje__44CA3770");

                    b.HasOne("Entities.Models.SchemeType", "SchemeType")
                        .WithMany("EmployeeComponents")
                        .HasForeignKey("SchemeTypeId")
                        .HasConstraintName("FK__EmployeeC__Schem__45BE5BA9");

                    b.Navigation("Component");

                    b.Navigation("CostCenter");

                    b.Navigation("Employee");

                    b.Navigation("PaymentDaysType");

                    b.Navigation("Project");

                    b.Navigation("SchemeType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Entities.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Entities.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Entities.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.AccountsReportChart", b =>
                {
                    b.Navigation("ComponentCreditAccounts");

                    b.Navigation("ComponentDebitAccounts");
                });

            modelBuilder.Entity("Entities.Models.AccountsReportChartType", b =>
                {
                    b.Navigation("AccountsReportCharts");
                });

            modelBuilder.Entity("Entities.Models.Coefficient", b =>
                {
                    b.Navigation("Components");
                });

            modelBuilder.Entity("Entities.Models.Component", b =>
                {
                    b.Navigation("EmployeeComponents");
                });

            modelBuilder.Entity("Entities.Models.CostCenter", b =>
                {
                    b.Navigation("EmployeeComponents");
                });

            modelBuilder.Entity("Entities.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Entities.Models.Employee", b =>
                {
                    b.Navigation("Calculations");

                    b.Navigation("EmployeeComponents");
                });

            modelBuilder.Entity("Entities.Models.EmployeeComponent", b =>
                {
                    b.Navigation("Calculations");
                });

            modelBuilder.Entity("Entities.Models.EmployeeGraceType", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Entities.Models.EmployeeType", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Entities.Models.PaymentDaysType", b =>
                {
                    b.Navigation("EmployeeComponents");
                });

            modelBuilder.Entity("Entities.Models.Project", b =>
                {
                    b.Navigation("EmployeeComponents");
                });

            modelBuilder.Entity("Entities.Models.SchemeType", b =>
                {
                    b.Navigation("Calculations");

                    b.Navigation("EmployeeComponents");

                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
