using System;
using System.Collections.Generic;
using back_end.Model;
using Microsoft.EntityFrameworkCore;

namespace back_end;

public partial class TattooThuBayContext : DbContext
{
    public TattooThuBayContext()
    {
    }

    public TattooThuBayContext(DbContextOptions<TattooThuBayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblArtist> TblArtists { get; set; }

    public virtual DbSet<TblBooking> TblBookings { get; set; }

    public virtual DbSet<TblBookingDetail> TblBookingDetails { get; set; }


    public virtual DbSet<TblCetificate> TblCetificates { get; set; }

    public virtual DbSet<TblFeedback> TblFeedbacks { get; set; }


    public virtual DbSet<TblManager> TblManagers { get; set; }

    public virtual DbSet<TblMember> TblMembers { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblSchedule> TblSchedules { get; set; }

    public virtual DbSet<TblService> TblServices { get; set; }

    public virtual DbSet<TblStudio> TblStudios { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblArtist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__tbl_Arti__25706B70A0840A69");

            entity.ToTable("tbl_Artist");

            entity.Property(e => e.ArtistId)
                .ValueGeneratedNever()
                .HasColumnName("ArtistID");
            entity.Property(e => e.CreateArtist).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.FisrtName).HasMaxLength(30);
            entity.Property(e => e.FullName).HasMaxLength(30);
            entity.Property(e => e.ImageArtist).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Member).WithMany(p => p.TblArtists)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Artist");
        });

        modelBuilder.Entity<TblBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__tbl_Book__73951ACD0B05FE6A");

            entity.ToTable("tbl_Booking");

            entity.Property(e => e.BookingId)
                .ValueGeneratedNever()
                .HasColumnName("BookingID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.StudioId).HasColumnName("StudioID");

            entity.HasOne(d => d.Member).WithMany(p => p.TblBookings)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Booking_Member");
        });

        modelBuilder.Entity<TblBookingDetail>(entity =>
        {
            entity.HasKey(e => e.BookingDetailId).HasName("PK__tbl_Book__8136D47AE147BE85");

            entity.ToTable("tbl_BookingDetail");

            entity.Property(e => e.BookingDetailId)
                .ValueGeneratedNever()
                .HasColumnName("BookingDetailID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Artist).WithMany(p => p.TblBookingDetails)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK_BookingDetail_Artist");

            entity.HasOne(d => d.Booking).WithMany(p => p.TblBookingDetails)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_BookingDetail_Booking");

            entity.HasOne(d => d.Service).WithMany(p => p.TblBookingDetails)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_BookingDetail_Service");
        });

        modelBuilder.Entity<TblCetificate>(entity =>
        {
            entity.HasKey(e => e.CetificateId).HasName("PK__tbl_Ceti__8D9CF4CC95F70B51");

            entity.ToTable("tbl_Cetificate");

            entity.Property(e => e.CetificateId).ValueGeneratedNever();
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Cerificate).HasMaxLength(100);

            entity.HasOne(d => d.Artist).WithMany(p => p.TblCetificates)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK_Cetificate");
        });

        modelBuilder.Entity<TblFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__tbl_Feed__6A4BEDF677E5E668");

            entity.ToTable("tbl_Feedback");

            entity.Property(e => e.FeedbackId)
                .ValueGeneratedNever()
                .HasColumnName("FeedbackID");
            entity.Property(e => e.FeedbackDate).HasColumnType("datetime");
            entity.Property(e => e.FeedbackDetail).HasMaxLength(200);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Member).WithMany(p => p.TblFeedbacks)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Feedback_Member");

            entity.HasOne(d => d.Service).WithMany(p => p.TblFeedbacks)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_Feedback_Service");
        });

        modelBuilder.Entity<TblManager>(entity =>
        {
            entity.HasKey(e => e.ManagerId).HasName("PK__tbl_Mana__3BA2AA81F8054CB6");

            entity.ToTable("tbl_Manager");

            entity.Property(e => e.ManagerId)
                .ValueGeneratedNever()
                .HasColumnName("ManagerID");
            entity.Property(e => e.FisrtName).HasMaxLength(20);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.ManagerPhone).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TblManagers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_tbl_Manager_tbl_User");
        });

        modelBuilder.Entity<TblMember>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__tbl_Memb__0CF04B38B951631F");

            entity.ToTable("tbl_Member");

            entity.Property(e => e.MemberId)
                .ValueGeneratedNever()
                .HasColumnName("MemberID");
            entity.Property(e => e.CreateMember).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.MemberName).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);

            entity.HasOne(d => d.User).WithMany(p => p.TblMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_tbl_Member_tbl_User");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tbl_Role__8AFACE3A6CA545D0");

            entity.ToTable("tbl_Role");

            entity.Property(e => e.RoleId)
                .HasMaxLength(20)
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<TblSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__tbl_Sche__9C8A5B4930B9EDE5");

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
            entity.HasKey(e => e.ServiceId).HasName("PK__tbl_Serv__C51BB0EA1BE3C153");

            entity.ToTable("tbl_Service");

            entity.Property(e => e.ServiceId)
                .ValueGeneratedNever()
                .HasColumnName("ServiceID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(30)
                .HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.ImageService).HasMaxLength(30);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ServiceItemId).HasColumnName("ServiceItemID");
            entity.Property(e => e.ServiceName).HasMaxLength(30);

            entity.HasOne(d => d.Artist).WithMany(p => p.TblServices)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK_Service_Artist");
        });

        modelBuilder.Entity<TblStudio>(entity =>
        {
            entity.HasKey(e => e.StudioId).HasName("PK__tbl_Stud__4ACC3B50463D2651");

            entity.ToTable("tbl_Studio");

            entity.Property(e => e.StudioId)
                .ValueGeneratedNever()
                .HasColumnName("StudioID");
            entity.Property(e => e.Address).HasMaxLength(30);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.StudioEmail).HasMaxLength(30);
            entity.Property(e => e.StudioName).HasMaxLength(30);
            entity.Property(e => e.StudioPhone).HasMaxLength(20);

            entity.HasOne(d => d.Manager).WithMany(p => p.TblStudios)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK_Studio_Manager");

            entity.HasOne(d => d.Service).WithMany(p => p.TblStudios)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_Studio_Service");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tbl_User__1788CC4CD86737A9");

            entity.ToTable("tbl_User");

            entity.Property(e => e.CreateUser).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(40);
            entity.Property(e => e.RoleId)
                .HasMaxLength(20)
                .HasColumnName("RoleID");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");
            entity.Property(e => e.Username).HasMaxLength(30);

            entity.HasOne(d => d.Role).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_tbl_User_tbl_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
