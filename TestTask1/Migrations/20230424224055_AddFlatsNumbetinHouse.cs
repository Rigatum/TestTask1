using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask1.Migrations
{
    /// <inheritdoc />
    public partial class AddFlatsNumbetinHouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FlatsNumber <= 100");

            migrationBuilder.RenameTable(
                name: "House",
                newName: "House",
                newSchema: "FlatsNumber <= 100");

            migrationBuilder.AddColumn<byte>(
                name: "FlatsNumber",
                schema: "FlatsNumber <= 100",
                table: "House",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlatsNumber",
                schema: "FlatsNumber <= 100",
                table: "House");

            migrationBuilder.RenameTable(
                name: "House",
                schema: "FlatsNumber <= 100",
                newName: "House");
        }
    }
}
