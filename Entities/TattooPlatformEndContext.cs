using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace back_end.Entities;

public partial class TattooPlatformEndContext : DbContext
{
    public TattooPlatformEndContext()
    {
    }

    public TattooPlatformEndContext(DbContextOptions<TattooPlatformEndContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblArtist> TblArtists { get; set; }

    public virtual DbSet<TblBooking> TblBookings { get; set; }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblFeedback> TblFeedbacks { get; set; }

    public virtual DbSet<TblManager> TblManagers { get; set; }

    public virtual DbSet<TblMember> TblMembers { get; set; }

    public virtual DbSet<TblPayment> TblPayments { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblSchedule> TblSchedules { get; set; }

    public virtual DbSet<TblService> TblServices { get; set; }

    public virtual DbSet<TblStudio> TblStudios { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=35.240.234.172;Initial Catalog=TattooPlatformEND;User ID=sa;Password=ArtTattoo123@;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblArtist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__tbl_Arti__25706B7077AD2D61");

            entity.ToTable("tbl_Artist");

            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.ArtistName).HasMaxLength(30);
            entity.Property(e => e.AvatarArtist).HasMaxLength(250);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TblArtists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_tbl_Artist_tbl_User");
        });

        modelBuilder.Entity<TblBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__tbl_Book__73951ACD60201975");

            entity.ToTable("tbl_Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StudioId).HasColumnName("StudioID");
            entity.Property(e => e.Total).HasColumnType("money");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Service).WithMany(p => p.TblBookings)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_tbl_Booking_tbl_Service");

            entity.HasOne(d => d.Studio).WithMany(p => p.TblBookings)
                .HasForeignKey(d => d.StudioId)
                .HasConstraintName("FK_tbl_Booking_tbl_Studio");

            entity.HasOne(d => d.User).WithMany(p => p.TblBookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Booking_Member");
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("tbl_Category");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__tbl_Feed__6A4BEDF6590185D6");

            entity.ToTable("tbl_Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.FeedbackDate).HasColumnType("datetime");
            entity.Property(e => e.FeedbackDetail).HasMaxLength(200);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Service).WithMany(p => p.TblFeedbacks)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_Feedback_Service");

            entity.HasOne(d => d.User).WithMany(p => p.TblFeedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_tbl_Feedback_tbl_User");
        });

        modelBuilder.Entity<TblManager>(entity =>
        {
            entity.HasKey(e => e.ManagerId).HasName("PK__tbl_Mana__3BA2AA8199ACDBAA");

            entity.ToTable("tbl_Manager");

            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.ManagerName).HasMaxLength(50);
            entity.Property(e => e.ManagerPhone).HasMaxLength(20);
            entity.Property(e => e.StudioId).HasColumnName("StudioID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Studio).WithMany(p => p.TblManagers)
                .HasForeignKey(d => d.StudioId)
                .HasConstraintName("FK_tbl_Manager_tbl_Studio");

            entity.HasOne(d => d.User).WithMany(p => p.TblManagers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Manager_User");
        });

        modelBuilder.Entity<TblMember>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__tbl_Memb__0CF04B38D2887CA5");

            entity.ToTable("tbl_Member");

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.MemberName).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TblMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Member");
        });

        modelBuilder.Entity<TblPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__tbl_Paym__9B556A58A3BA4C59");

            entity.ToTable("tbl_Payment");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("PaymentID");
            entity.Property(e => e.Bonus).HasColumnType("money");
            entity.Property(e => e.Currency).HasMaxLength(20);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PaymentAmount).HasColumnType("money");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Member).WithMany(p => p.TblPayments)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Payment_Member");

            entity.HasOne(d => d.Service).WithMany(p => p.TblPayments)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_Payment_Service");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tbl_Role__8AFACE3A673EF5AE");

            entity.ToTable("tbl_Role");

            entity.Property(e => e.RoleId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<TblSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__tbl_Sche__9C8A5B49E160657F");

            entity.ToTable("tbl_Schedule");

            entity.Property(e => e.ScheduleId).ValueGeneratedNever();
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.FreeTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Artist).WithMany(p => p.TblSchedules)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK_Schedule");
        });

        modelBuilder.Entity<TblService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__tbl_Serv__C51BB0EA154B50EB");

            entity.ToTable("tbl_Service");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CategoryID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ImageService).IsUnicode(false);
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudioId).HasColumnName("StudioID");

            entity.HasOne(d => d.Category).WithMany(p => p.TblServices)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_tbl_Service_tbl_Category");

            entity.HasOne(d => d.Studio).WithMany(p => p.TblServices)
                .HasForeignKey(d => d.StudioId)
                .HasConstraintName("FK_tbl_Service_tbl_Studio");
        });

        modelBuilder.Entity<TblStudio>(entity =>
        {
            entity.HasKey(e => e.StudioId).HasName("PK__tbl_Stud__4ACC3B504091F053");

            entity.ToTable("tbl_Studio");

            entity.Property(e => e.StudioId).HasColumnName("StudioID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EndTime)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OpenTime)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StudioEmail).HasMaxLength(30);
            entity.Property(e => e.StudioName).HasMaxLength(30);
            entity.Property(e => e.StudioPhone).HasMaxLength(20);
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tbl_User__1788CC4CF5BC973D");

            entity.ToTable("tbl_User", tb => tb.HasTrigger("trg_AddUserToRoleTable"));

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreateUser).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(60);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RoleId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("RoleID");
            entity.Property(e => e.UserName).HasMaxLength(30);

            entity.HasOne(d => d.Role).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_tbl_User_tbl_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
