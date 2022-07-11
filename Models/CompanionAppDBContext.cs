using CompanionApp.Models.Identity_models;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CompanionApp.Models
{
    public partial class CompanionAppDBContext : IdentityDbContext<Profile,AppRole,Guid> 
    {
        public CompanionAppDBContext()
        {
        }

        public CompanionAppDBContext(DbContextOptions<CompanionAppDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseTakenBy> CourseTakenBy { get; set; } = null!;
        public virtual DbSet<Following> Followings { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Profile> Profiles { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = SchoolmanAPI; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            }
            optionsBuilder.UseExceptionProcessor();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("COMMENTS", "CompanionApp");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_CREATED");

                entity.Property(e => e.PostId).HasColumnName("postID");

                entity.Property(e => e.Text).HasColumnName("TEXT");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_COMMENTS_POST");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COMMENTS_PROFILE");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Crn)
                    .HasName("PK__COURSE__C1F887FFF352CD8D");

                entity.ToTable("COURSE", "CompanionApp");

                entity.Property(e => e.Crn)
                    .ValueGeneratedNever()
                    .HasColumnName("CRN");

                entity.Property(e => e.Attribute)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ATTRIBUTE");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Corequisites).HasColumnType("text");

                entity.Property(e => e.Credits).HasColumnName("CREDITS");

                entity.Property(e => e.Days1)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("DAYS_1");

                entity.Property(e => e.Days2)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("DAYS_2");

                entity.Property(e => e.EndTime1)
                    .HasColumnType("time(0)")
                    .HasColumnName("END_TIME_1");

                entity.Property(e => e.EndTime2)
                    .HasColumnType("time(0)")
                    .HasColumnName("END_TIME_2");

                entity.Property(e => e.Instructor1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INSTRUCTOR_1");

                entity.Property(e => e.Instructor2)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INSTRUCTOR_2");

                entity.Property(e => e.Levels)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LEVELS");

                entity.Property(e => e.Location1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION_1");

                entity.Property(e => e.Location2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION_2");

                entity.Property(e => e.MutualExclusion)
                    .HasColumnType("text")
                    .HasColumnName("Mutual_Exclusion");

                entity.Property(e => e.Prerequisites).HasColumnType("text");

                entity.Property(e => e.Restrictions).HasColumnType("text");

                entity.Property(e => e.Section)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SECTION");

                entity.Property(e => e.SemesterId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("semesterID");

                entity.Property(e => e.StartTime1)
                    .HasColumnType("time(0)")
                    .HasColumnName("START_TIME_1");

                entity.Property(e => e.StartTime2)
                    .HasColumnType("time(0)")
                    .HasColumnName("START_TIME_2");

                entity.Property(e => e.Subject)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");

                entity.Property(e => e.Type1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPE_1");

                entity.Property(e => e.Type2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPE_2");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COURSE_SEMESTER");
            });

            modelBuilder.Entity<CourseTakenBy>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CCrn, e.SemesterId });

                entity.ToTable("COURSE_TAKEN_BY", "CompanionApp");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.CCrn).HasColumnName("cCRN");

                entity.Property(e => e.SemesterId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("semesterID");

                entity.Property(e => e.Grade)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("GRADE");

                entity.HasOne(d => d.CCrnNavigation)
                    .WithMany(p => p.CourseTakenBy)
                    .HasForeignKey(d => d.CCrn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__COURSE_TAK__cCRN__395884C4");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.CourseTakenBy)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COURSE_TAKEN_BY_SEMESTER");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CourseTakenBy)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COURSE_TAKEN_BY_PROFILE");
            });

            modelBuilder.Entity<Following>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.IsFollowing });

                entity.ToTable("FOLLOWING", "CompanionApp");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.IsFollowing).HasColumnName("Is_Following");

                entity.Property(e => e.DateFollowed)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_FOLLOWED");

                entity.HasOne(d => d.IsFollowingNavigation)
                    .WithMany(p => p.FollowingIsFollowingNavigations)
                    .HasForeignKey(d => d.IsFollowing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FOLLOWING_PROFILE1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FollowingUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_FOLLOWING_PROFILE");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserId });

                entity.ToTable("LIKES", "CompanionApp");

                entity.Property(e => e.PostId).HasColumnName("postID");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.DateLiked)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_LIKED");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_LIKES_POST");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LIKES_PROFILE");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("POST", "CompanionApp");

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
                    .HasConstraintName("FK_POST_PROFILE");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("PROFILE", "CompanionApp");

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
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("SEMESTER", "CompanionApp");

                entity.Property(e => e.Id)
                    .HasMaxLength(6)
                    .IsUnicode(false)
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
