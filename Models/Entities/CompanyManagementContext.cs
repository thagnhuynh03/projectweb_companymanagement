using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace huynhkimthang_0145_Final_LTC_.Models.Entities;

public partial class CompanyManagementContext : DbContext
{
    public CompanyManagementContext()
    {
    }

    public CompanyManagementContext(DbContextOptions<CompanyManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<AnnouncementCategory> AnnouncementCategories { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostCategory> PostCategories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-A252DME\\THANG;Initial Catalog=CompanyManagement;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnId).HasName("PK__Announce__1C67F94B61DAE422");

            entity.ToTable("Announcement");

            entity.HasOne(d => d.AnnCate).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.AnnCateId)
                .HasConstraintName("FK_AnnoucementCategory");

            entity.HasOne(d => d.Post).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_Post_Ann");
        });

        modelBuilder.Entity<AnnouncementCategory>(entity =>
        {
            entity.HasKey(e => e.AnnCateId).HasName("PK__Announce__B41C41AE57BA3B97");

            entity.ToTable("AnnouncementCategory");

            entity.Property(e => e.AnnCateDesc).HasMaxLength(100);
            entity.Property(e => e.AnnCateName).HasMaxLength(50);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepId).HasName("PK__Departme__DB9CAA5F1080FAFA");

            entity.ToTable("Department");

            entity.Property(e => e.DepDesc).HasMaxLength(100);
            entity.Property(e => e.DepName).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB997471DF86");

            entity.ToTable("Employee");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmpName).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Dep).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepId)
                .HasConstraintName("FK_Department");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Role");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__AA126018B6DBC2C6");

            entity.ToTable("Post");

            entity.Property(e => e.Img1).IsUnicode(false);
            entity.Property(e => e.Img2).IsUnicode(false);
            entity.Property(e => e.Img3).IsUnicode(false);
            entity.Property(e => e.ThumbnailImg).IsUnicode(false);

            entity.HasOne(d => d.Emp).WithMany(p => p.Posts)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK_Employee");

            entity.HasOne(d => d.PostTypeNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PostType)
                .HasConstraintName("FK_PostCategory");
        });

        modelBuilder.Entity<PostCategory>(entity =>
        {
            entity.HasKey(e => e.PostCateId).HasName("PK__PostCate__A0BFCB7F94CB63BD");

            entity.ToTable("PostCategory");

            entity.Property(e => e.PostCateDesc).HasMaxLength(100);
            entity.Property(e => e.PostCateName).HasMaxLength(20);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A6C3C091D");

            entity.ToTable("Role");

            entity.Property(e => e.RoleDecs).HasMaxLength(100);
            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.SchId).HasName("PK__Schedule__CAD9870B7FDD1A99");

            entity.ToTable("Schedule");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Loacation)
                .HasMaxLength(200)
                .HasColumnName("loacation");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Post).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_Post_Sch");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
