using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanionApp.Migrations
{
    public partial class RemovedUnnecessaryAttributedFromProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__PROFILE__161CF72470A5A43A",
                schema: "CompanionApp",
                table: "PROFILE");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "CompanionApp",
                table: "PROFILE",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                schema: "CompanionApp",
                table: "PROFILE",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "UQ__PROFILE__161CF72470A5A43A",
                schema: "CompanionApp",
                table: "PROFILE",
                column: "EMAIL",
                unique: true,
                filter: "[EMAIL] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__PROFILE__161CF72470A5A43A",
                schema: "CompanionApp",
                table: "PROFILE");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "CompanionApp",
                table: "PROFILE",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                schema: "CompanionApp",
                table: "PROFILE",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ__PROFILE__161CF72470A5A43A",
                schema: "CompanionApp",
                table: "PROFILE",
                column: "EMAIL",
                unique: true);
        }
    }
}
