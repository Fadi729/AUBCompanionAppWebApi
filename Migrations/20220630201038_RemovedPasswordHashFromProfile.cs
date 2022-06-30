using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanionApp.Migrations
{
    public partial class RemovedPasswordHashFromProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PASSWORD_HASH",
                schema: "CompanionApp",
                table: "PROFILE",
                newName: "PasswordHash");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                schema: "CompanionApp",
                table: "PROFILE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(64)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 64,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "CompanionApp",
                table: "PROFILE",
                newName: "PASSWORD_HASH");

            migrationBuilder.AlterColumn<string>(
                name: "PASSWORD_HASH",
                schema: "CompanionApp",
                table: "PROFILE",
                type: "char(64)",
                unicode: false,
                fixedLength: true,
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
