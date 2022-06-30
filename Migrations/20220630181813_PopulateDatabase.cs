using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanionApp.Migrations
{
    public partial class PopulateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CompanionApp");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PROFILE",
                schema: "CompanionApp",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PASSWORD_HASH = table.Column<string>(type: "char(64)", unicode: false, fixedLength: true, maxLength: 64, nullable: true),
                    FIRST_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LAST_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    EMAIL = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MAJOR = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CLASS = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROFILE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SEMESTER",
                schema: "CompanionApp",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    TITLE = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    YEAR = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEMESTER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FOLLOWING",
                schema: "CompanionApp",
                columns: table => new
                {
                    userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Is_Following = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DATE_FOLLOWED = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOLLOWING", x => new { x.userID, x.Is_Following });
                    table.ForeignKey(
                        name: "FK_FOLLOWING_PROFILE",
                        column: x => x.userID,
                        principalSchema: "CompanionApp",
                        principalTable: "PROFILE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FOLLOWING_PROFILE1",
                        column: x => x.Is_Following,
                        principalSchema: "CompanionApp",
                        principalTable: "PROFILE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "POST",
                schema: "CompanionApp",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TEXT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ATTACHMENT = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DATE_CREATED = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POST", x => x.ID);
                    table.ForeignKey(
                        name: "FK_POST_PROFILE",
                        column: x => x.userID,
                        principalSchema: "CompanionApp",
                        principalTable: "PROFILE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COURSE",
                schema: "CompanionApp",
                columns: table => new
                {
                    CRN = table.Column<int>(type: "int", nullable: false),
                    TITLE = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    SUBJECT = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CREDITS = table.Column<byte>(type: "tinyint", nullable: false),
                    SECTION = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ATTRIBUTE = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LEVELS = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DAYS_1 = table.Column<string>(type: "varchar(7)", unicode: false, maxLength: 7, nullable: true),
                    START_TIME_1 = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    END_TIME_1 = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    LOCATION_1 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TYPE_1 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    INSTRUCTOR_1 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DAYS_2 = table.Column<string>(type: "varchar(7)", unicode: false, maxLength: 7, nullable: true),
                    START_TIME_2 = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    END_TIME_2 = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    LOCATION_2 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TYPE_2 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    INSTRUCTOR_2 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    semesterID = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    Prerequisites = table.Column<string>(type: "text", nullable: true),
                    Corequisites = table.Column<string>(type: "text", nullable: true),
                    Mutual_Exclusion = table.Column<string>(type: "text", nullable: true),
                    Restrictions = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__COURSE__C1F887FFF352CD8D", x => x.CRN);
                    table.ForeignKey(
                        name: "FK_COURSE_SEMESTER",
                        column: x => x.semesterID,
                        principalSchema: "CompanionApp",
                        principalTable: "SEMESTER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "COMMENTS",
                schema: "CompanionApp",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    postID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TEXT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DATE_CREATED = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COMMENTS_POST",
                        column: x => x.postID,
                        principalSchema: "CompanionApp",
                        principalTable: "POST",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COMMENTS_PROFILE",
                        column: x => x.userID,
                        principalSchema: "CompanionApp",
                        principalTable: "PROFILE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LIKES",
                schema: "CompanionApp",
                columns: table => new
                {
                    postID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DATE_LIKED = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LIKES", x => new { x.postID, x.userID });
                    table.ForeignKey(
                        name: "FK_LIKES_POST",
                        column: x => x.postID,
                        principalSchema: "CompanionApp",
                        principalTable: "POST",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LIKES_PROFILE",
                        column: x => x.userID,
                        principalSchema: "CompanionApp",
                        principalTable: "PROFILE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "COURSE_TAKEN_BY",
                schema: "CompanionApp",
                columns: table => new
                {
                    userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cCRN = table.Column<int>(type: "int", nullable: false),
                    semesterID = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    GRADE = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSE_TAKEN_BY", x => new { x.userID, x.cCRN, x.semesterID });
                    table.ForeignKey(
                        name: "FK__COURSE_TAK__cCRN__395884C4",
                        column: x => x.cCRN,
                        principalSchema: "CompanionApp",
                        principalTable: "COURSE",
                        principalColumn: "CRN");
                    table.ForeignKey(
                        name: "FK_COURSE_TAKEN_BY_PROFILE",
                        column: x => x.userID,
                        principalSchema: "CompanionApp",
                        principalTable: "PROFILE",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_COURSE_TAKEN_BY_SEMESTER",
                        column: x => x.semesterID,
                        principalSchema: "CompanionApp",
                        principalTable: "SEMESTER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTS_postID",
                schema: "CompanionApp",
                table: "COMMENTS",
                column: "postID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTS_userID",
                schema: "CompanionApp",
                table: "COMMENTS",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_semesterID",
                schema: "CompanionApp",
                table: "COURSE",
                column: "semesterID");

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_TAKEN_BY_cCRN",
                schema: "CompanionApp",
                table: "COURSE_TAKEN_BY",
                column: "cCRN");

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_TAKEN_BY_semesterID",
                schema: "CompanionApp",
                table: "COURSE_TAKEN_BY",
                column: "semesterID");

            migrationBuilder.CreateIndex(
                name: "IX_FOLLOWING_Is_Following",
                schema: "CompanionApp",
                table: "FOLLOWING",
                column: "Is_Following");

            migrationBuilder.CreateIndex(
                name: "IX_LIKES_userID",
                schema: "CompanionApp",
                table: "LIKES",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_POST_userID",
                schema: "CompanionApp",
                table: "POST",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "UQ__PROFILE__161CF72470A5A43A",
                schema: "CompanionApp",
                table: "PROFILE",
                column: "EMAIL",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "COMMENTS",
                schema: "CompanionApp");

            migrationBuilder.DropTable(
                name: "COURSE_TAKEN_BY",
                schema: "CompanionApp");

            migrationBuilder.DropTable(
                name: "FOLLOWING",
                schema: "CompanionApp");

            migrationBuilder.DropTable(
                name: "LIKES",
                schema: "CompanionApp");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "COURSE",
                schema: "CompanionApp");

            migrationBuilder.DropTable(
                name: "POST",
                schema: "CompanionApp");

            migrationBuilder.DropTable(
                name: "SEMESTER",
                schema: "CompanionApp");

            migrationBuilder.DropTable(
                name: "PROFILE",
                schema: "CompanionApp");
        }
    }
}
