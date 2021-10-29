using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Entities.Models;

#nullable disable

namespace Entities
{
    public partial class SynergyContext : DbContext
    {
        public SynergyContext()
        {
        }

        public SynergyContext(DbContextOptions<SynergyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bnkkop> Bnkkops { get; set; }
        public virtual DbSet<Hrjbtl> Hrjbtls { get; set; }
        public virtual DbSet<Humre> Humres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=AZENAISHVILI1;database=811;Trusted_Connection=True;User ID=PayrollModule;Password=NewPass1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bnkkop>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("bnkkop");

                entity.HasIndex(e => new { e.CodeDc, e.Crdnr, e.Debnr, e.BankRek }, "bnkkdc")
                    .IsUnique();

                entity.HasIndex(e => new { e.BankRek, e.CodeDc, e.Crdnr, e.Debnr }, "bnkkop")
                    .IsUnique()
                    .IsClustered();

                entity.HasIndex(e => e.CntId, "ix_cnt_id");

                entity.HasIndex(e => e.Crdnr, "ix_crdnr");

                entity.HasIndex(e => e.Debnr, "ix_debnr");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BankRek)
                    .HasMaxLength(34)
                    .HasColumnName("bank_rek");

                entity.Property(e => e.CntId).HasColumnName("cnt_id");

                entity.Property(e => e.CodeDc)
                    .HasMaxLength(1)
                    .HasColumnName("code_dc")
                    .IsFixedLength(true);

                entity.Property(e => e.Crdnr)
                    .HasMaxLength(6)
                    .HasColumnName("crdnr")
                    .IsFixedLength(true);

                entity.Property(e => e.Debnr)
                    .HasMaxLength(6)
                    .HasColumnName("debnr")
                    .IsFixedLength(true);

                entity.Property(e => e.Syscreated)
                    .HasColumnType("datetime")
                    .HasColumnName("syscreated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syscreator).HasColumnName("syscreator");

                entity.Property(e => e.Sysguid)
                    .HasColumnName("sysguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Sysmodified)
                    .HasColumnType("datetime")
                    .HasColumnName("sysmodified")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Sysmodifier).HasColumnName("sysmodifier");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");
            });

            modelBuilder.Entity<Hrjbtl>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("hrjbtl");

                entity.HasIndex(e => e.JobTitle, "hrjbtl")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BeroepCd)
                    .HasMaxLength(4)
                    .HasColumnName("beroep_cd")
                    .IsFixedLength(true);

                entity.Property(e => e.CodeFnc)
                    .HasMaxLength(12)
                    .HasColumnName("code_fnc");

                entity.Property(e => e.DelDocName)
                    .HasMaxLength(128)
                    .HasColumnName("del_doc_name");

                entity.Property(e => e.DelDocPath)
                    .HasMaxLength(128)
                    .HasColumnName("del_doc_path");

                entity.Property(e => e.Descr50)
                    .HasMaxLength(50)
                    .HasColumnName("descr50");

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.Exttar).HasColumnName("exttar");

                entity.Property(e => e.Inttar).HasColumnName("inttar");

                entity.Property(e => e.JobGrp)
                    .HasMaxLength(10)
                    .HasColumnName("job_grp")
                    .IsFixedLength(true);

                entity.Property(e => e.JobLevel).HasColumnName("job_level");

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("job_title");

                entity.Property(e => e.Kmrateint).HasColumnName("kmrateint");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.Prodpct).HasColumnName("prodpct");

                entity.Property(e => e.Productiv).HasColumnName("productiv");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Schaalcode)
                    .HasMaxLength(2)
                    .HasColumnName("schaalcode")
                    .IsFixedLength(true);

                entity.Property(e => e.Schaalsrt)
                    .HasMaxLength(2)
                    .HasColumnName("schaalsrt")
                    .IsFixedLength(true);

                entity.Property(e => e.Syscreated)
                    .HasColumnType("datetime")
                    .HasColumnName("syscreated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syscreator).HasColumnName("syscreator");

                entity.Property(e => e.Sysguid)
                    .HasColumnName("sysguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Sysmodified)
                    .HasColumnType("datetime")
                    .HasColumnName("sysmodified")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Sysmodifier).HasColumnName("sysmodifier");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");

                entity.Property(e => e.VertGrp)
                    .HasMaxLength(10)
                    .HasColumnName("vert_grp")
                    .IsFixedLength(true);

                entity.Property(e => e.WkcpRisk)
                    .HasMaxLength(4)
                    .HasColumnName("wkcp_risk")
                    .IsFixedLength(true);

                entity.Property(e => e.Xdefratelv)
                    .HasMaxLength(1)
                    .HasColumnName("xdefratelv")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Humre>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("humres");

                entity.HasIndex(e => new { e.Comp, e.Crdnr }, "IDX_Humres_Comp_Crdnr");

                entity.HasIndex(e => e.ProcessedByBackgroundJob, "ProcessedByBackgroundJob");

                entity.HasIndex(e => e.UsrId, "UQ_humres_usr_id")
                    .IsUnique();

                entity.HasIndex(e => e.Crdnr, "crdnr");

                entity.HasIndex(e => new { e.BtwNummer, e.Id }, "hrbtw")
                    .IsUnique();

                entity.HasIndex(e => new { e.Buyer, e.ResId }, "hrsbuy")
                    .IsUnique();

                entity.HasIndex(e => new { e.Comp, e.Id }, "hrscmp")
                    .IsUnique();

                entity.HasIndex(e => new { e.Fullname, e.Id }, "hrsfnm")
                    .IsUnique();

                entity.HasIndex(e => new { e.Payempl, e.ResId }, "hrspay")
                    .IsUnique();

                entity.HasIndex(e => new { e.PrafdCode, e.Payempl, e.ResId }, "hrspra")
                    .IsUnique();

                entity.HasIndex(e => new { e.Projempl, e.ResId }, "hrsprj")
                    .IsUnique();

                entity.HasIndex(e => new { e.Representative, e.ResId }, "hrsrep")
                    .IsUnique();

                entity.HasIndex(e => new { e.SurName, e.Id }, "hrssnm")
                    .IsUnique();

                entity.HasIndex(e => new { e.SocsecNr, e.Payempl, e.ResId }, "hrssoc")
                    .IsUnique();

                entity.HasIndex(e => new { e.UsrId, e.Id }, "hrsusr")
                    .IsUnique();

                entity.HasIndex(e => new { e.UsrId2, e.Id }, "hrsusr2")
                    .IsUnique();

                entity.HasIndex(e => e.ResId, "humres")
                    .IsUnique()
                    .IsClustered();

                entity.HasIndex(e => new { e.EmpType, e.EmpStat, e.BackOfficeBlocked }, "ix_humres_BackOfficeBlocked");

                entity.HasIndex(e => new { e.EmpType, e.EmpStat, e.Blocked }, "ix_humres_typestatus");

                entity.HasIndex(e => new { e.Timestamp, e.EmpType }, "ix_timestamp");

                entity.HasIndex(e => new { e.ReptoId, e.Id }, "repto_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Absent).HasColumnName("absent");

                entity.Property(e => e.AddrNo)
                    .HasMaxLength(10)
                    .HasColumnName("addr_no")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.AddrXtra)
                    .HasMaxLength(10)
                    .HasColumnName("addr_xtra")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.AdjustedHireDate).HasColumnType("datetime");

                entity.Property(e => e.Adres1)
                    .HasMaxLength(60)
                    .HasColumnName("adres1")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Adres2)
                    .HasMaxLength(60)
                    .HasColumnName("adres2")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.AdvCode)
                    .HasMaxLength(1)
                    .HasColumnName("adv_code")
                    .IsFixedLength(true);

                entity.Property(e => e.Affix)
                    .HasMaxLength(64)
                    .HasColumnName("affix")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.ApplicationStage)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.Assistant).HasColumnName("assistant");

                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.AttachmentType)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.Bankac0)
                    .HasMaxLength(34)
                    .HasColumnName("bankac_0");

                entity.Property(e => e.Bankac1)
                    .HasMaxLength(34)
                    .HasColumnName("bankac_1");

                entity.Property(e => e.BirthAffix)
                    .HasMaxLength(64)
                    .HasColumnName("birth_affix")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.BirthPrefix)
                    .HasMaxLength(64)
                    .HasColumnName("birth_prefix")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Blocked).HasColumnName("blocked");

                entity.Property(e => e.BtwNummer)
                    .HasMaxLength(20)
                    .HasColumnName("btw_nummer");

                entity.Property(e => e.Buyer).HasColumnName("buyer");

                entity.Property(e => e.ChildBirthDate1).HasColumnType("datetime");

                entity.Property(e => e.ChildBirthDate2).HasColumnType("datetime");

                entity.Property(e => e.ChildBirthDate3).HasColumnType("datetime");

                entity.Property(e => e.ChildBirthDate4).HasColumnType("datetime");

                entity.Property(e => e.ChildBirthDate5).HasColumnType("datetime");

                entity.Property(e => e.ChildName1)
                    .HasMaxLength(64)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.ChildName2)
                    .HasMaxLength(64)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.ChildName3)
                    .HasMaxLength(64)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.ChildName4)
                    .HasMaxLength(64)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.ChildName5)
                    .HasMaxLength(64)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Classification)
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.CmpWwn).HasColumnName("cmp_wwn");

                entity.Property(e => e.Comp)
                    .HasMaxLength(3)
                    .HasColumnName("comp")
                    .IsFixedLength(true);

                entity.Property(e => e.ContEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("cont_end_date");

                entity.Property(e => e.Costcenter)
                    .HasMaxLength(8)
                    .HasColumnName("costcenter")
                    .IsFixedLength(true);

                entity.Property(e => e.CrcardExpd)
                    .HasColumnType("datetime")
                    .HasColumnName("crcard_expd");

                entity.Property(e => e.CrcardNo)
                    .HasMaxLength(30)
                    .HasColumnName("crcard_no");

                entity.Property(e => e.CrcardType)
                    .HasMaxLength(1)
                    .HasColumnName("crcard_type")
                    .IsFixedLength(true);

                entity.Property(e => e.Crdnr)
                    .HasMaxLength(30)
                    .HasColumnName("crdnr");

                entity.Property(e => e.DateOfDead).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.EmpStat)
                    .HasMaxLength(1)
                    .HasColumnName("emp_stat")
                    .IsFixedLength(true);

                entity.Property(e => e.EmpStatd)
                    .HasColumnType("datetime")
                    .HasColumnName("emp_statd");

                entity.Property(e => e.EmpType)
                    .HasMaxLength(1)
                    .HasColumnName("emp_type")
                    .IsFixedLength(true);

                entity.Property(e => e.ExtraCode)
                    .HasMaxLength(12)
                    .HasColumnName("extra_code");

                entity.Property(e => e.ExtraText)
                    .HasMaxLength(20)
                    .HasColumnName("extra_text");

                entity.Property(e => e.Exttar).HasColumnName("exttar");

                entity.Property(e => e.Faxnr)
                    .HasMaxLength(25)
                    .HasColumnName("faxnr");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(64)
                    .HasColumnName("first_name")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Freefield1)
                    .HasMaxLength(50)
                    .HasColumnName("freefield1")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Freefield10)
                    .HasColumnType("datetime")
                    .HasColumnName("freefield10");

                entity.Property(e => e.Freefield11).HasColumnName("freefield11");

                entity.Property(e => e.Freefield12).HasColumnName("freefield12");

                entity.Property(e => e.Freefield13).HasColumnName("freefield13");

                entity.Property(e => e.Freefield14).HasColumnName("freefield14");

                entity.Property(e => e.Freefield15).HasColumnName("freefield15");

                entity.Property(e => e.Freefield16).HasColumnName("freefield16");

                entity.Property(e => e.Freefield17).HasColumnName("freefield17");

                entity.Property(e => e.Freefield18).HasColumnName("freefield18");

                entity.Property(e => e.Freefield19).HasColumnName("freefield19");

                entity.Property(e => e.Freefield2)
                    .HasMaxLength(50)
                    .HasColumnName("freefield2")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Freefield20).HasColumnName("freefield20");

                entity.Property(e => e.Freefield3)
                    .HasMaxLength(50)
                    .HasColumnName("freefield3")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Freefield4)
                    .HasMaxLength(50)
                    .HasColumnName("freefield4")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Freefield5)
                    .HasMaxLength(50)
                    .HasColumnName("freefield5")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Freefield6)
                    .HasColumnType("datetime")
                    .HasColumnName("freefield6");

                entity.Property(e => e.Freefield7)
                    .HasColumnType("datetime")
                    .HasColumnName("freefield7");

                entity.Property(e => e.Freefield8)
                    .HasColumnType("datetime")
                    .HasColumnName("freefield8");

                entity.Property(e => e.Freefield9)
                    .HasColumnType("datetime")
                    .HasColumnName("freefield9");

                entity.Property(e => e.Fte).HasColumnName("fte");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(64)
                    .HasColumnName("fullname")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Funcnivo)
                    .HasMaxLength(4)
                    .HasColumnName("funcnivo")
                    .IsFixedLength(true);

                entity.Property(e => e.GebLdat)
                    .HasColumnType("datetime")
                    .HasColumnName("geb_ldat");

                entity.Property(e => e.GebPl)
                    .HasMaxLength(30)
                    .HasColumnName("geb_pl")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.HomePageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("HomePageURL");

                entity.Property(e => e.Identiteit)
                    .HasMaxLength(20)
                    .HasColumnName("identiteit");

                entity.Property(e => e.Initialen)
                    .HasMaxLength(10)
                    .HasColumnName("initialen")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.InternetUrl)
                    .HasMaxLength(255)
                    .HasColumnName("InternetURL");

                entity.Property(e => e.Internetac)
                    .HasMaxLength(60)
                    .HasColumnName("internetac")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Inttar).HasColumnName("inttar");

                entity.Property(e => e.IsPersonalAccount).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsoTaalcd)
                    .HasMaxLength(3)
                    .HasColumnName("iso_taalcd")
                    .IsFixedLength(true);

                entity.Property(e => e.ItemCode).HasMaxLength(30);

                entity.Property(e => e.JobCategory)
                    .HasMaxLength(10)
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.JobLevel).HasColumnName("job_level");

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(12)
                    .HasColumnName("job_title");

                entity.Property(e => e.Kamer)
                    .HasMaxLength(10)
                    .HasColumnName("kamer")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.KstdrCode)
                    .HasMaxLength(8)
                    .HasColumnName("kstdr_code")
                    .IsFixedLength(true);

                entity.Property(e => e.LandIso)
                    .HasMaxLength(3)
                    .HasColumnName("land_iso")
                    .IsFixedLength(true);

                entity.Property(e => e.LandIso2)
                    .HasMaxLength(3)
                    .HasColumnName("land_iso2")
                    .IsFixedLength(true);

                entity.Property(e => e.Ldatindienst)
                    .HasColumnType("datetime")
                    .HasColumnName("ldatindienst");

                entity.Property(e => e.Ldatuitdienst)
                    .HasColumnType("datetime")
                    .HasColumnName("ldatuitdienst");

                entity.Property(e => e.Loc)
                    .HasMaxLength(10)
                    .HasColumnName("loc")
                    .IsFixedLength(true);

                entity.Property(e => e.MaidenName)
                    .HasMaxLength(30)
                    .HasColumnName("maiden_name")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Mail)
                    .HasMaxLength(100)
                    .HasColumnName("mail");

                entity.Property(e => e.MainLoc)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.MarDate)
                    .HasColumnType("datetime")
                    .HasColumnName("mar_date");

                entity.Property(e => e.MarStat)
                    .HasMaxLength(1)
                    .HasColumnName("mar_stat")
                    .IsFixedLength(true);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(30)
                    .HasColumnName("middle_name")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.MobileShort)
                    .HasMaxLength(15)
                    .HasColumnName("mobile_short");

                entity.Property(e => e.MsnId)
                    .HasMaxLength(60)
                    .HasColumnName("MsnID");

                entity.Property(e => e.Mv1)
                    .HasMaxLength(1)
                    .HasColumnName("mv1")
                    .IsFixedLength(true);

                entity.Property(e => e.Nat)
                    .HasMaxLength(3)
                    .HasColumnName("nat")
                    .IsFixedLength(true);

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.OfficialName)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasDefaultValueSql("(N'01')")
                    .IsFixedLength(true);

                entity.Property(e => e.OldNtfullName)
                    .HasMaxLength(64)
                    .HasColumnName("OldNTFullName");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Partner)
                    .HasMaxLength(64)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.PartnerBirthDate).HasColumnType("datetime");

                entity.Property(e => e.Payempl).HasColumnName("payempl");

                entity.Property(e => e.PictureThumbnailFilename).HasMaxLength(128);

                entity.Property(e => e.Picturefilename)
                    .HasMaxLength(128)
                    .HasColumnName("picturefilename");

                entity.Property(e => e.Postcode)
                    .HasMaxLength(20)
                    .HasColumnName("postcode")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.PrafdCode)
                    .HasMaxLength(8)
                    .HasColumnName("prafd_code")
                    .IsFixedLength(true);

                entity.Property(e => e.Predcode)
                    .HasMaxLength(4)
                    .HasColumnName("predcode")
                    .IsFixedLength(true);

                entity.Property(e => e.Predcode2)
                    .HasMaxLength(64)
                    .HasColumnName("predcode2");

                entity.Property(e => e.Prefix)
                    .HasMaxLength(64)
                    .HasColumnName("prefix")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.ProbEndd)
                    .HasColumnType("datetime")
                    .HasColumnName("prob_endd");

                entity.Property(e => e.ProbPer)
                    .HasMaxLength(2)
                    .HasColumnName("prob_per")
                    .IsFixedLength(true);

                entity.Property(e => e.ProbPert)
                    .HasMaxLength(1)
                    .HasColumnName("prob_pert")
                    .IsFixedLength(true);

                entity.Property(e => e.Projempl).HasColumnName("projempl");

                entity.Property(e => e.PurLimit).HasColumnName("pur_limit");

                entity.Property(e => e.Race)
                    .HasMaxLength(2)
                    .HasColumnName("race")
                    .IsFixedLength(true);

                entity.Property(e => e.Rating)
                    .HasMaxLength(3)
                    .HasColumnName("rating")
                    .IsFixedLength(true);

                entity.Property(e => e.ReasonResign)
                    .HasMaxLength(120)
                    .HasColumnName("reason_resign")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Representative).HasColumnName("representative");

                entity.Property(e => e.ReptoId).HasColumnName("repto_id");

                entity.Property(e => e.ResId).HasColumnName("res_id");

                entity.Property(e => e.Rmalimit).HasColumnName("RMALimit");

                entity.Property(e => e.SignatureFile).HasMaxLength(128);

                entity.Property(e => e.SipUri)
                    .HasMaxLength(255)
                    .HasColumnName("SipURI");

                entity.Property(e => e.SkypeId)
                    .HasMaxLength(60)
                    .HasColumnName("SkypeID");

                entity.Property(e => e.SlipTekst)
                    .HasMaxLength(210)
                    .HasColumnName("slip_tekst");

                entity.Property(e => e.SocsecNr)
                    .HasMaxLength(50)
                    .HasColumnName("socsec_nr");

                entity.Property(e => e.StateCode)
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.SurName)
                    .HasMaxLength(64)
                    .HasColumnName("sur_name")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Syscreated)
                    .HasColumnType("datetime")
                    .HasColumnName("syscreated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syscreator).HasColumnName("syscreator");

                entity.Property(e => e.Sysguid)
                    .HasColumnName("sysguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Sysmodified)
                    .HasColumnType("datetime")
                    .HasColumnName("sysmodified")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Sysmodifier).HasColumnName("sysmodifier");

                entity.Property(e => e.Task)
                    .HasMaxLength(90)
                    .HasColumnName("task")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.TelnrPrv)
                    .HasMaxLength(25)
                    .HasColumnName("telnr_prv");

                entity.Property(e => e.TelnrPrv2)
                    .HasMaxLength(25)
                    .HasColumnName("telnr_prv2");

                entity.Property(e => e.TelnrWerk)
                    .HasMaxLength(25)
                    .HasColumnName("telnr_werk");

                entity.Property(e => e.TelnrWerk2)
                    .HasMaxLength(25)
                    .HasColumnName("telnr_werk2");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");

                entity.Property(e => e.Toestel)
                    .HasMaxLength(10)
                    .HasColumnName("toestel")
                    .IsFixedLength(true);

                entity.Property(e => e.UitkCode)
                    .HasMaxLength(3)
                    .HasColumnName("uitk_code")
                    .IsFixedLength(true);

                entity.Property(e => e.UserGroup)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'Baco')");

                entity.Property(e => e.UsrId)
                    .HasMaxLength(255)
                    .HasColumnName("usr_id");

                entity.Property(e => e.UsrId2)
                    .HasMaxLength(255)
                    .HasColumnName("usr_id2");

                entity.Property(e => e.VacancyQuantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.VacancyStartDate).HasColumnType("datetime");

                entity.Property(e => e.VacancyType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(N'I')")
                    .IsFixedLength(true);

                entity.Property(e => e.Valcode)
                    .HasMaxLength(3)
                    .HasColumnName("valcode")
                    .IsFixedLength(true);

                entity.Property(e => e.Woonpl)
                    .HasMaxLength(30)
                    .HasColumnName("woonpl")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.Workstat)
                    .HasMaxLength(20)
                    .HasColumnName("workstat")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");

                entity.Property(e => e.WorkstatAddr)
                    .HasMaxLength(57)
                    .HasColumnName("workstat_addr")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            });

            modelBuilder.HasSequence<int>("SEQ_actlogCode")
                .StartsAt(1000)
                .HasMin(0);

            modelBuilder.HasSequence<int>("SEQ_actrequestCode")
                .StartsAt(1000)
                .HasMin(0);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
