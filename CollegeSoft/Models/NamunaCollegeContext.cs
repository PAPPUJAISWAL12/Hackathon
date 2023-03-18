using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CollegeSoft.Models;

public partial class NamunaCollegeContext : DbContext
{
    public NamunaCollegeContext()
    {
    }

    public NamunaCollegeContext(DbContextOptions<NamunaCollegeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<DocumentView> DocumentViews { get; set; }

    public virtual DbSet<Fee> Fees { get; set; }

    public virtual DbSet<FeeDetail> FeeDetails { get; set; }

    public virtual DbSet<FeeDetailsView> FeeDetailsViews { get; set; }

    public virtual DbSet<FeePrint> FeePrints { get; set; }

    public virtual DbSet<FeePrintView> FeePrintViews { get; set; }

    public virtual DbSet<FeeView> FeeViews { get; set; }

    public virtual DbSet<PrograInfoView> PrograInfoViews { get; set; }

    public virtual DbSet<ProgramInfo> ProgramInfos { get; set; }

    public virtual DbSet<Reception> Receptions { get; set; }

    public virtual DbSet<ReceptionView> ReceptionViews { get; set; }

    public virtual DbSet<RoleList> RoleLists { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentView> StudentViews { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubjectsView> SubjectsViews { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeacherView> TeacherViews { get; set; }

    public virtual DbSet<UploadFile> UploadFiles { get; set; }

    public virtual DbSet<UploadFileView> UploadFileViews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserRoleView> UserRoleViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=Conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PK__class__C1F8DC39803610FF");

            entity.ToTable("class");

            entity.Property(e => e.Cid).HasColumnName("CId");
            entity.Property(e => e.Cname).HasMaxLength(20);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocId).HasName("PK__Document__3EF188AD9172E1CA");

            entity.ToTable("Document");

            entity.HasOne(d => d.User).WithMany(p => p.Documents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Document__UserId__4F47C5E3");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Document__516F03B5645A3D04");

            entity.ToTable("DocumentType");

            entity.Property(e => e.DocumetCat).HasMaxLength(100);
        });

        modelBuilder.Entity<DocumentView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("DocumentView");

            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
        });

        modelBuilder.Entity<Fee>(entity =>
        {
            entity.HasKey(e => e.FeeId).HasName("PK__Fee__E09FF2033CFEB926");

            entity.ToTable("Fee");

            entity.Property(e => e.FeeId).HasColumnName("feeId");
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.EntryDate).HasColumnType("date");
            entity.Property(e => e.EntryTime).HasMaxLength(20);
            entity.Property(e => e.Examfee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FiscalYear).HasMaxLength(20);
            entity.Property(e => e.MonthlyFeeAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StdId).HasColumnName("stdId");
            entity.Property(e => e.YearlyAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.YearlyDiscount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CancelledByNavigation).WithMany(p => p.FeeCancelledByNavigations)
                .HasForeignKey(d => d.CancelledBy)
                .HasConstraintName("FK__Fee__CancelledBy__3E1D39E1");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.FeeEntryByNavigations)
                .HasForeignKey(d => d.EntryBy)
                .HasConstraintName("FK__Fee__EntryBy__3D2915A8");

            entity.HasOne(d => d.Std).WithMany(p => p.Fees)
                .HasForeignKey(d => d.StdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fee__stdId__3C34F16F");
        });

        modelBuilder.Entity<FeeDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__feeDetai__135C316DF28409CE");

            entity.ToTable("feeDetails");

            entity.Property(e => e.FeeId).HasColumnName("feeId");
            entity.Property(e => e.FeeStatus).HasMaxLength(50);
            entity.Property(e => e.PaidAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RemainingAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmt).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Fee).WithMany(p => p.FeeDetails)
                .HasForeignKey(d => d.FeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__feeDetail__feeId__42E1EEFE");
        });

        modelBuilder.Entity<FeeDetailsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FeeDetailsView");

            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.CancelledUser).HasMaxLength(20);
            entity.Property(e => e.Cname).HasMaxLength(20);
            entity.Property(e => e.EntryUser).HasMaxLength(20);
            entity.Property(e => e.Examfee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FeeId).HasColumnName("feeId");
            entity.Property(e => e.FeeStatus).HasMaxLength(50);
            entity.Property(e => e.FiscalYear).HasMaxLength(20);
            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.MonthlyFeeAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaidAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.RemainingAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
            entity.Property(e => e.YearlyAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.YearlyDiscount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<FeePrint>(entity =>
        {
            entity.HasKey(e => e.PrintId).HasName("PK__FeePrint__26C7BA7DF1A7A38B");

            entity.ToTable("FeePrint");

            entity.Property(e => e.PrintDate).HasColumnType("date");
            entity.Property(e => e.PrintTime).HasMaxLength(20);

            entity.HasOne(d => d.Detail).WithMany(p => p.FeePrints)
                .HasForeignKey(d => d.DetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FeePrint__Detail__46B27FE2");

            entity.HasOne(d => d.PrintUser).WithMany(p => p.FeePrints)
                .HasForeignKey(d => d.PrintUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FeePrint__PrintU__45BE5BA9");
        });

        modelBuilder.Entity<FeePrintView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FeePrintView");

            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.CancelledUser).HasMaxLength(20);
            entity.Property(e => e.Cname).HasMaxLength(20);
            entity.Property(e => e.EntryUser).HasMaxLength(20);
            entity.Property(e => e.Examfee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FeeStatus).HasMaxLength(50);
            entity.Property(e => e.FiscalYear).HasMaxLength(20);
            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.MonthlyFeeAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaidAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.PrintDate).HasColumnType("date");
            entity.Property(e => e.PrintTime).HasMaxLength(20);
            entity.Property(e => e.RemainingAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
            entity.Property(e => e.YearlyAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.YearlyDiscount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<FeeView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FeeView");

            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.CancelledUser).HasMaxLength(20);
            entity.Property(e => e.Cname).HasMaxLength(20);
            entity.Property(e => e.EntryUser).HasMaxLength(20);
            entity.Property(e => e.Examfee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FeeId).HasColumnName("feeId");
            entity.Property(e => e.FiscalYear).HasMaxLength(20);
            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.MonthlyFeeAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.StdId).HasColumnName("stdId");
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
            entity.Property(e => e.YearlyAmt).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.YearlyDiscount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PrograInfoView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("PrograInfoView");

            entity.Property(e => e.CancelledBy).HasMaxLength(20);
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.EndTime).HasMaxLength(20);
            entity.Property(e => e.EntryUser).HasMaxLength(20);
            entity.Property(e => e.Pdescription).HasColumnName("PDescription");
            entity.Property(e => e.Pid)
                .ValueGeneratedOnAdd()
                .HasColumnName("PID");
            entity.Property(e => e.Pname)
                .HasMaxLength(100)
                .HasColumnName("PName");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.StartTime).HasMaxLength(20);
        });

        modelBuilder.Entity<ProgramInfo>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__ProgramI__C57755207AB2019D");

            entity.ToTable("ProgramInfo");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.EndTime).HasMaxLength(20);
            entity.Property(e => e.EntryDate).HasColumnType("date");
            entity.Property(e => e.Pdescription).HasColumnName("PDescription");
            entity.Property(e => e.Pname)
                .HasMaxLength(100)
                .HasColumnName("PName");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.StartTime).HasMaxLength(20);

            entity.HasOne(d => d.Cancelled).WithMany(p => p.ProgramInfoCancelleds)
                .HasForeignKey(d => d.CancelledId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProgramIn__Cance__25518C17");

            entity.HasOne(d => d.User).WithMany(p => p.ProgramInfoUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProgramIn__UserI__245D67DE");
        });

        modelBuilder.Entity<Reception>(entity =>
        {
            entity.HasKey(e => e.Rid).HasName("PK__Receptio__CAFF40D26947389C");

            entity.ToTable("Reception");

            entity.Property(e => e.Rid).HasColumnName("RId");
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.EntryDate).HasColumnType("date");
            entity.Property(e => e.EntryTime).HasMaxLength(20);
            entity.Property(e => e.FiscalYear).HasMaxLength(20);
            entity.Property(e => e.PersonAddress).HasMaxLength(1);
            entity.Property(e => e.PersonName).HasMaxLength(1);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.Purpose).HasMaxLength(1);
            entity.Property(e => e.ReceptionStatus).HasMaxLength(20);

            entity.HasOne(d => d.CancelledBy).WithMany(p => p.ReceptionCancelledBies)
                .HasForeignKey(d => d.CancelledById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reception__Cance__208CD6FA");

            entity.HasOne(d => d.User).WithMany(p => p.ReceptionUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reception__UserI__1F98B2C1");
        });

        modelBuilder.Entity<ReceptionView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ReceptionView");

            entity.Property(e => e.CancelledBy).HasMaxLength(20);
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.EntryBy).HasMaxLength(20);
            entity.Property(e => e.EntryDate).HasColumnType("date");
            entity.Property(e => e.EntryTime).HasMaxLength(20);
            entity.Property(e => e.FiscalYear).HasMaxLength(20);
            entity.Property(e => e.PersonAddress).HasMaxLength(1);
            entity.Property(e => e.PersonName).HasMaxLength(1);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.Purpose).HasMaxLength(1);
            entity.Property(e => e.ReceptionStatus).HasMaxLength(20);
            entity.Property(e => e.Rid)
                .ValueGeneratedOnAdd()
                .HasColumnName("RId");
        });

        modelBuilder.Entity<RoleList>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__RoleList__8AFACE1A9105B229");

            entity.ToTable("RoleList");

            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StdId).HasName("PK__Student__55DCAE1F52536136");

            entity.ToTable("Student");

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Cid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__Cid__70DDC3D8");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__UserId__71D1E811");
        });

        modelBuilder.Entity<StudentView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("StudentView");

            entity.Property(e => e.Cname).HasMaxLength(20);
            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubId).HasName("PK__Subjects__4D9BB84ADA897E81");

            entity.Property(e => e.Cid).HasColumnName("CId");
            entity.Property(e => e.SubName).HasMaxLength(1);

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.Cid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subjects__CId__2BFE89A6");
        });

        modelBuilder.Entity<SubjectsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("SubjectsView");

            entity.Property(e => e.Cid).HasColumnName("CId");
            entity.Property(e => e.Cname).HasMaxLength(20);
            entity.Property(e => e.SubName).HasMaxLength(1);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Tid).HasName("PK__Teacher__C451DB315740AFD4");

            entity.ToTable("Teacher");

            entity.Property(e => e.Tpost)
                .HasMaxLength(20)
                .HasColumnName("TPost");

            entity.HasOne(d => d.User).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Teacher__UserId__619B8048");
        });

        modelBuilder.Entity<TeacherView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TeacherView");

            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.Tpost)
                .HasMaxLength(20)
                .HasColumnName("TPost");
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
        });

        modelBuilder.Entity<UploadFile>(entity =>
        {
            entity.HasKey(e => e.UploadId).HasName("PK__UploadFi__6D16C84D46354C20");

            entity.ToTable("UploadFile");

            entity.Property(e => e.DocFile).HasColumnName("docFile");

            entity.HasOne(d => d.Doc).WithMany(p => p.UploadFiles)
                .HasForeignKey(d => d.DocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UploadFil__DocId__5F7E2DAC");

            entity.HasOne(d => d.Type).WithMany(p => p.UploadFiles)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UploadFil__TypeI__607251E5");
        });

        modelBuilder.Entity<UploadFileView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UploadFileView");

            entity.Property(e => e.DocFile).HasColumnName("docFile");
            entity.Property(e => e.DocumetCat).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C037B93E5");

            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.LoginStatus)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.Upassword)
                .HasMaxLength(20)
                .HasColumnName("UPassword");
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A352289B1C9");

            entity.ToTable("UserRole");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRole__RoleId__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRole__UserId__4222D4EF");
        });

        modelBuilder.Entity<UserRoleView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UserRoleView");

            entity.Property(e => e.RoleName).HasMaxLength(20);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
