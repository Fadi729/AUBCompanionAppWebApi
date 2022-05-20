using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CompanionApp.Models
{
    public partial class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext()
        {
        }

        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseTakenBy> CourseTakenBies { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Profile> Profiles { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=CompanionAppDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("CompanionApp");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserId });

                entity.ToTable("COMMENTS");

                entity.Property(e => e.PostId).HasColumnName("postID");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_CREATED");

                entity.Property(e => e.Text).HasColumnName("TEXT");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__COMMENTS__postID__44CA3770");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Crn)
                    .HasName("PK__COURSE__C1F887FFF352CD8D");

                entity.ToTable("COURSE");

                entity.Property(e => e.Crn)
                    .ValueGeneratedNever()
                    .HasColumnName("CRN");

                entity.Property(e => e.Attribute)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ATTRIBUTE");

                entity.Property(e => e.Code).HasColumnName("CODE");

                entity.Property(e => e.Credits).HasColumnName("CREDITS");

                entity.Property(e => e.Days)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("DAYS");

                entity.Property(e => e.EndTime)
                    .HasColumnType("time(0)")
                    .HasColumnName("END_TIME");

                entity.Property(e => e.Instructor)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INSTRUCTOR");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.StartTime)
                    .HasColumnType("time(0)")
                    .HasColumnName("START_TIME");

                entity.Property(e => e.Subject)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");
            });

            modelBuilder.Entity<CourseTakenBy>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CCrn, e.SemesterId });

                entity.ToTable("COURSE_TAKEN_BY");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.CCrn).HasColumnName("cCRN");

                entity.Property(e => e.SemesterId).HasColumnName("semesterID");

                entity.Property(e => e.Grade)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("GRADE");

                entity.HasOne(d => d.CCrnNavigation)
                    .WithMany(p => p.CourseTakenBies)
                    .HasForeignKey(d => d.CCrn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__COURSE_TAK__cCRN__395884C4");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.CourseTakenBies)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__COURSE_TA__semes__3A4CA8FD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CourseTakenBies)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COURSE_TAKEN_BY_PROFILE");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserId });

                entity.ToTable("LIKES");

                entity.Property(e => e.PostId).HasColumnName("postID");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LIKES__postID__489AC854");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("POST");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Attachment).HasColumnName("ATTACHMENT");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_CREATED");

                entity.Property(e => e.Text).HasColumnName("TEXT");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POST_PROFILE");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("PROFILE");

                entity.HasIndex(e => e.Email, "UQ__PROFILE__161CF72470A5A43A")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Class)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CLASS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.Major)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MAJOR");

                entity.HasMany(d => d.UserFollowings)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "Following",
                        l => l.HasOne<Profile>().WithMany().HasForeignKey("UserFollowingid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__FOLLOWING__userF__3E1D39E1"),
                        r => r.HasOne<Profile>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_FOLLOWING_PROFILE"),
                        j =>
                        {
                            j.HasKey("UserId", "UserFollowingid");

                            j.ToTable("FOLLOWING");

                            j.IndexerProperty<Guid>("UserId").HasColumnName("userID");

                            j.IndexerProperty<Guid>("UserFollowingid").HasColumnName("userFOLLOWINGID");
                        });

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.UserFollowings)
                    .UsingEntity<Dictionary<string, object>>(
                        "Following",
                        l => l.HasOne<Profile>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_FOLLOWING_PROFILE"),
                        r => r.HasOne<Profile>().WithMany().HasForeignKey("UserFollowingid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__FOLLOWING__userF__3E1D39E1"),
                        j =>
                        {
                            j.HasKey("UserId", "UserFollowingid");

                            j.ToTable("FOLLOWING");

                            j.IndexerProperty<Guid>("UserId").HasColumnName("userID");

                            j.IndexerProperty<Guid>("UserFollowingid").HasColumnName("userFOLLOWINGID");
                        });
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("SEMESTER");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");

                entity.Property(e => e.Year)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("YEAR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
