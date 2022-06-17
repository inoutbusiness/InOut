using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InOut.Infrastructure.Migrations
{
    public partial class AddFieldCodeInProductEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Products",
                type: "VARCHAR(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Locations",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(220)",
                oldMaxLength: 220);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Locations",
                type: "VARCHAR(220)",
                maxLength: 220,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200);
        }
    }
}
