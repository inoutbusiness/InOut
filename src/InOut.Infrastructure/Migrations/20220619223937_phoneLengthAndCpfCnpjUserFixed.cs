using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InOut.Infrastructure.Migrations
{
    public partial class phoneLengthAndCpfCnpjUserFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "VARCHAR(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "CpfCnpj",
                table: "Users",
                type: "VARCHAR(18)",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(14)",
                oldMaxLength: 14);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "VARCHAR(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(16)",
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "CpfCnpj",
                table: "Users",
                type: "VARCHAR(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(18)",
                oldMaxLength: 18);
        }
    }
}
