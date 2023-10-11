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

    public virtual DbSet<TblBookingDetail> TblBookingDetails { get; set; }

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
        => optionsBuilder.UseSqlServer("Data Source=35.240.234.172;Initial Catalog=TattooPlatformEND;User ID=sa;Password=ArtTattoo123@;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblArtist>(entity =>
        {
            entity.HasKey(e => e.ArtistID).HasName("PK__tbl_Arti__25706B7077AD2D61");

            entity.ToTable("tbl_Artist");

            entity.Property(e => e.ArtistID).HasColumnName("ArtistID");
            entity.Property(e => e.ArtistName).HasMaxLength(30);
            entity.Property(e => e.AvatarArtist).HasMaxLength(250);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            entity.Property(e => e.UserID).HasColumnName("UserID");
        });

        modelBuilder.Entity<TblBooking>(entity =>
        {
            entity.HasKey(e => e.BookingID).HasName("PK__tbl_Book__73951ACDD0CFC33D");

            entity.ToTable("tbl_Booking");

            entity.Property(e => e.BookingID)
                .ValueGeneratedNever()
                .HasColumnName("BookingID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.MemberID).HasColumnName("MemberID");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ServiceID).HasColumnName("ServiceID");
            entity.Property(e => e.StudioID).HasColumnName("StudioID");
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.Member).WithMany(p => p.TblBookings)
                .HasForeignKey(d => d.MemberID)
                .HasConstraintName("FK_Booking_Member");

            entity.HasOne(d => d.Service).WithMany(p => p.TblBookings)
                .HasForeignKey(d => d.ServiceID)
                .HasConstraintName("FK_tbl_Booking_tbl_Service");

            entity.HasOne(d => d.Studio).WithMany(p => p.TblBookings)
                .HasForeignKey(d => d.StudioID)
                .HasConstraintName("FK_tbl_Booking_tbl_Studio");
        });

        modelBuilder.Entity<TblBookingDetail>(entity =>
        {
            entity.HasKey(e => e.BookingDetailID).HasName("PK__tbl_Book__8136D47AEB50265B");

            entity.ToTable("tbl_BookingDetail");

            entity.Property(e => e.BookingDetailID)
                .ValueGeneratedNever()
                .HasColumnName("BookingDetailID");
            entity.Property(e => e.ArtistID).HasColumnName("ArtistID");
            entity.Property(e => e.BookingID).HasColumnName("BookingID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ServiceID).HasColumnName("ServiceID");

            entity.HasOne(d => d.Artist).WithMany(p => p.TblBookingDetails)
                .HasForeignKey(d => d.ArtistID)
                .HasConstraintName("FK_BookingDetail_Artist");

            entity.HasOne(d => d.Booking).WithMany(p => p.TblBookingDetails)
                .HasForeignKey(d => d.BookingID)
                .HasConstraintName("FK_BookingDetail_Booking");

            entity.HasOne(d => d.Service).WithMany(p => p.TblBookingDetails)
                .HasForeignKey(d => d.ServiceID)
                .HasConstraintName("FK_BookingDetail_Service");
        });

        modelBuilder.Entity<TblFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackID).HasName("PK__tbl_Feed__6A4BEDF6DF00DDBA");

            entity.ToTable("tbl_Feedback");

            entity.Property(e => e.FeedbackID)
                .ValueGeneratedNever()
                .HasColumnName("FeedbackID");
            entity.Property(e => e.FeedbackDate).HasColumnType("datetime");
            entity.Property(e => e.FeedbackDetail).HasMaxLength(200);
            entity.Property(e => e.MemberID).HasColumnName("MemberID");
            entity.Property(e => e.ServiceID).HasColumnName("ServiceID");

            entity.HasOne(d => d.Member).WithMany(p => p.TblFeedbacks)
                .HasForeignKey(d => d.MemberID)
                .HasConstraintName("FK_Feedback_Member");

            entity.HasOne(d => d.Service).WithMany(p => p.TblFeedbacks)
                .HasForeignKey(d => d.ServiceID)
                .HasConstraintName("FK_Feedback_Service");
        });

        modelBuilder.Entity<TblManager>(entity =>
        {
            entity.HasKey(e => e.ManagerID).HasName("PK__tbl_Mana__3BA2AA8199ACDBAA");

            entity.ToTable("tbl_Manager");

            entity.Property(e => e.ManagerID).HasColumnName("ManagerID");
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.ManagerName).HasMaxLength(50);
            entity.Property(e => e.ManagerPhone).HasMaxLength(20);
            entity.Property(e => e.UserID).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TblManagers)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("FK_Manager_User");
        });

        modelBuilder.Entity<TblMember>(entity =>
        {
            entity.HasKey(e => e.MemberID).HasName("PK__tbl_Memb__0CF04B38D2887CA5");

            entity.ToTable("tbl_Member");

            entity.Property(e => e.MemberID).HasColumnName("MemberID");
            entity.Property(e => e.MemberName).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            entity.Property(e => e.UserID).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TblMembers)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("FK_Member");
        });

        modelBuilder.Entity<TblPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentID).HasName("PK__tbl_Paym__9B556A582576E420");

            entity.ToTable("tbl_Payment");

            entity.Property(e => e.PaymentID)
                .ValueGeneratedNever()
                .HasColumnName("PaymentID");
            entity.Property(e => e.Bonus).HasColumnType("money");
            entity.Property(e => e.Currency).HasMaxLength(20);
            entity.Property(e => e.MemberID).HasColumnName("MemberID");
            entity.Property(e => e.PaymentAmount).HasColumnType("money");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
            entity.Property(e => e.ServiceID).HasColumnName("ServiceID");

            entity.HasOne(d => d.Member).WithMany(p => p.TblPayments)
                .HasForeignKey(d => d.MemberID)
                .HasConstraintName("FK_Payment_Member");

            entity.HasOne(d => d.Service).WithMany(p => p.TblPayments)
                .HasForeignKey(d => d.ServiceID)
                .HasConstraintName("FK_Payment_Service");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleID).HasName("PK__tbl_Role__8AFACE3A673EF5AE");

            entity.ToTable("tbl_Role");

            entity.Property(e => e.RoleID)
                .HasMaxLength(20)
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<TblSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleID).HasName("PK__tbl_Sche__9C8A5B4980845A92");

            entity.ToTable("tbl_Schedule");

            entity.Property(e => e.ScheduleID).ValueGeneratedNever();
            entity.Property(e => e.ArtistID).HasColumnName("ArtistID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.FreeTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Artist).WithMany(p => p.TblSchedules)
                .HasForeignKey(d => d.ArtistID)
                .HasConstraintName("FK_Schedule");
        });

        modelBuilder.Entity<TblService>(entity =>
        {
            entity.HasKey(e => e.ServiceID).HasName("PK__tbl_Serv__C51BB0EA154B50EB");

            entity.ToTable("tbl_Service");

            entity.Property(e => e.ServiceID).HasColumnName("ServiceID");
            entity.Property(e => e.ArtistID).HasColumnName("ArtistID");
            entity.Property(e => e.CategoryID)
                .HasMaxLength(30)
                .HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ImageService).HasMaxLength(100);
            entity.Property(e => e.Price).HasMaxLength(10);
            entity.Property(e => e.ServiceName).HasMaxLength(30);
            entity.Property(e => e.StudioID).HasColumnName("StudioID");

            entity.HasOne(d => d.Artist).WithMany(p => p.TblServices)
                .HasForeignKey(d => d.ArtistID)
                .HasConstraintName("FK_Service_Artist");

            entity.HasOne(d => d.Studio).WithMany(p => p.TblServices)
                .HasForeignKey(d => d.StudioID)
                .HasConstraintName("FK_Service_Studio");
        });

        modelBuilder.Entity<TblStudio>(entity =>
        {
            entity.HasKey(e => e.StudioID).HasName("PK__tbl_Stud__4ACC3B504091F053");

            entity.ToTable("tbl_Studio");

            entity.Property(e => e.StudioID).HasColumnName("StudioID");
            entity.Property(e => e.Address).HasMaxLength(30);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ManagerID).HasColumnName("ManagerID");
            entity.Property(e => e.StudioEmail).HasMaxLength(30);
            entity.Property(e => e.StudioName).HasMaxLength(30);
            entity.Property(e => e.StudioPhone).HasMaxLength(20);

            entity.HasOne(d => d.Manager).WithMany(p => p.TblStudios)
                .HasForeignKey(d => d.ManagerID)
                .HasConstraintName("FK_Studio_Manager");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK__tbl_User__1788CC4CF5BC973D");

            entity.ToTable("tbl_User", tb => tb.HasTrigger("Trig_Add_User_By_Role"));

            entity.Property(e => e.UserID).HasColumnName("UserID");
            entity.Property(e => e.CreateUser).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(60);
            entity.Property(e => e.RoleID)
                .HasMaxLength(20)
                .HasColumnName("RoleID");
            entity.Property(e => e.UserName).HasMaxLength(30);

            entity.HasOne(d => d.Role).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.RoleID)
                .HasConstraintName("FK_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
