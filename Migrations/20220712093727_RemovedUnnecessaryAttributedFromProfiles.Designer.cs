﻿// <auto-generated />
using System;
using CompanionApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CompanionApp.Migrations
{
    [DbContext(typeof(CompanionAppDBContext))]
    [Migration("20220712093727_RemovedUnnecessaryAttributedFromProfiles")]
    partial class RemovedUnnecessaryAttributedFromProfiles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CompanionApp.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime")
                        .HasColumnName("DATE_CREATED");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("postID");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("userID");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("COMMENTS", "CompanionApp");
                });

            modelBuilder.Entity("CompanionApp.Models.Course", b =>
                {
                    b.Property<int>("Crn")
                        .HasColumnType("int")
                        .HasColumnName("CRN");

                    b.Property<string>("Attribute")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ATTRIBUTE");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("CODE");

                    b.Property<string>("Corequisites")
                        .HasColumnType("text");

                    b.Property<byte>("Credits")
                        .HasColumnType("tinyint")
                        .HasColumnName("CREDITS");

                    b.Property<string>("Days1")
                        .HasMaxLength(7)
                        .IsUnicode(false)
                        .HasColumnType("varchar(7)")
                        .HasColumnName("DAYS_1");

                    b.Property<string>("Days2")
                        .HasMaxLength(7)
                        .IsUnicode(false)
                        .HasColumnType("varchar(7)")
                        .HasColumnName("DAYS_2");

                    b.Property<TimeSpan?>("EndTime1")
                        .HasColumnType("time(0)")
                        .HasColumnName("END_TIME_1");

                    b.Property<TimeSpan?>("EndTime2")
                        .HasColumnType("time(0)")
                        .HasColumnName("END_TIME_2");

                    b.Property<string>("Instructor1")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("INSTRUCTOR_1");

                    b.Property<string>("Instructor2")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("INSTRUCTOR_2");

                    b.Property<string>("Levels")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("LEVELS");

                    b.Property<string>("Location1")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LOCATION_1");

                    b.Property<string>("Location2")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LOCATION_2");

                    b.Property<string>("MutualExclusion")
                        .HasColumnType("text")
                        .HasColumnName("Mutual_Exclusion");

                    b.Property<string>("Prerequisites")
                        .HasColumnType("text");

                    b.Property<string>("Restrictions")
                        .HasColumnType("text");

                    b.Property<string>("Section")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("SECTION");

                    b.Property<string>("SemesterId")
                        .IsRequired()
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)")
                        .HasColumnName("semesterID");

                    b.Property<TimeSpan?>("StartTime1")
                        .HasColumnType("time(0)")
                        .HasColumnName("START_TIME_1");

                    b.Property<TimeSpan?>("StartTime2")
                        .HasColumnType("time(0)")
                        .HasColumnName("START_TIME_2");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("SUBJECT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("TITLE");

                    b.Property<string>("Type1")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TYPE_1");

                    b.Property<string>("Type2")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TYPE_2");

                    b.HasKey("Crn")
                        .HasName("PK__COURSE__C1F887FFF352CD8D");

                    b.HasIndex("SemesterId");

                    b.ToTable("COURSE", "CompanionApp");
                });

            modelBuilder.Entity("CompanionApp.Models.CourseTakenBy", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("userID");

                    b.Property<int>("CCrn")
                        .HasColumnType("int")
                        .HasColumnName("cCRN");

                    b.Property<string>("SemesterId")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)")
                        .HasColumnName("semesterID");

                    b.Property<string>("Grade")
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("GRADE");

                    b.HasKey("UserId", "CCrn", "SemesterId");

                    b.HasIndex("CCrn");

                    b.HasIndex("SemesterId");

                    b.ToTable("COURSE_TAKEN_BY", "CompanionApp");
                });

            modelBuilder.Entity("CompanionApp.Models.Following", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("userID");

                    b.Property<Guid>("IsFollowing")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Is_Following");

                    b.Property<DateTime?>("DateFollowed")
                        .HasColumnType("datetime")
                        .HasColumnName("DATE_FOLLOWED");

                    b.HasKey("UserId", "IsFollowing");

                    b.HasIndex("IsFollowing");

                    b.ToTable("FOLLOWING", "CompanionApp");
                });

            modelBuilder.Entity("CompanionApp.Models.Identity_models.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("CompanionApp.Models.Like", b =>
                {
                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("postID");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("userID");

                    b.Property<DateTime>("DateLiked")
                        .HasColumnType("datetime")
                        .HasColumnName("DATE_LIKED");

                    b.HasKey("PostId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("LIKES", "CompanionApp");
                });

            modelBuilder.Entity("CompanionApp.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<byte[]>("Attachment")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("ATTACHMENT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime")
                        .HasColumnName("DATE_CREATED");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("userID");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("POST", "CompanionApp");
                });

            modelBuilder.Entity("CompanionApp.Models.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Class")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CLASS");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("EMAIL");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("FIRST_NAME");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LAST_NAME");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Major")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("MAJOR");

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

                    b.HasIndex(new[] { "Email" }, "UQ__PROFILE__161CF72470A5A43A")
                        .IsUnique()
                        .HasFilter("[EMAIL] IS NOT NULL");

                    b.ToTable("PROFILE", "CompanionApp");
                });

            modelBuilder.Entity("CompanionApp.Models.Semester", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)")
                        .HasColumnName("ID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TITLE");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("YEAR");

                    b.HasKey("Id");

                    b.ToTable("SEMESTER", "CompanionApp");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CompanionApp.Models.Comment", b =>
                {
                    b.HasOne("CompanionApp.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_COMMENTS_POST");

                    b.HasOne("CompanionApp.Models.Profile", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_COMMENTS_PROFILE");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CompanionApp.Models.Course", b =>
                {
                    b.HasOne("CompanionApp.Models.Semester", "Semester")
                        .WithMany("Courses")
                        .HasForeignKey("SemesterId")
                        .IsRequired()
                        .HasConstraintName("FK_COURSE_SEMESTER");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("CompanionApp.Models.CourseTakenBy", b =>
                {
                    b.HasOne("CompanionApp.Models.Course", "CCrnNavigation")
                        .WithMany("CourseTakenBy")
                        .HasForeignKey("CCrn")
                        .IsRequired()
                        .HasConstraintName("FK__COURSE_TAK__cCRN__395884C4");

                    b.HasOne("CompanionApp.Models.Semester", "Semester")
                        .WithMany("CourseTakenBy")
                        .HasForeignKey("SemesterId")
                        .IsRequired()
                        .HasConstraintName("FK_COURSE_TAKEN_BY_SEMESTER");

                    b.HasOne("CompanionApp.Models.Profile", "User")
                        .WithMany("CourseTakenBy")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_COURSE_TAKEN_BY_PROFILE");

                    b.Navigation("CCrnNavigation");

                    b.Navigation("Semester");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CompanionApp.Models.Following", b =>
                {
                    b.HasOne("CompanionApp.Models.Profile", "IsFollowingNavigation")
                        .WithMany("FollowingIsFollowingNavigations")
                        .HasForeignKey("IsFollowing")
                        .IsRequired()
                        .HasConstraintName("FK_FOLLOWING_PROFILE1");

                    b.HasOne("CompanionApp.Models.Profile", "User")
                        .WithMany("FollowingUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_FOLLOWING_PROFILE");

                    b.Navigation("IsFollowingNavigation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CompanionApp.Models.Like", b =>
                {
                    b.HasOne("CompanionApp.Models.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_LIKES_POST");

                    b.HasOne("CompanionApp.Models.Profile", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_LIKES_PROFILE");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CompanionApp.Models.Post", b =>
                {
                    b.HasOne("CompanionApp.Models.Profile", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_POST_PROFILE");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("CompanionApp.Models.Identity_models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("CompanionApp.Models.Profile", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("CompanionApp.Models.Profile", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("CompanionApp.Models.Identity_models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanionApp.Models.Profile", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("CompanionApp.Models.Profile", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CompanionApp.Models.Course", b =>
                {
                    b.Navigation("CourseTakenBy");
                });

            modelBuilder.Entity("CompanionApp.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("CompanionApp.Models.Profile", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("CourseTakenBy");

                    b.Navigation("FollowingIsFollowingNavigations");

                    b.Navigation("FollowingUsers");

                    b.Navigation("Likes");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("CompanionApp.Models.Semester", b =>
                {
                    b.Navigation("CourseTakenBy");

                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
