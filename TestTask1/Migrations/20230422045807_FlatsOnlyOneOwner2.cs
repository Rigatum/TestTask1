using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask1.Migrations
{
    /// <inheritdoc />
    public partial class FlatsOnlyOneOwner2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owner_Flat_FlatID",
                table: "Owner");

            migrationBuilder.AlterColumn<int>(
                name: "FlatID",
                table: "Owner",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Owner_Flat_FlatID",
                table: "Owner",
                column: "FlatID",
                principalTable: "Flat",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owner_Flat_FlatID",
                table: "Owner");

            migrationBuilder.AlterColumn<int>(
                name: "FlatID",
                table: "Owner",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Owner_Flat_FlatID",
                table: "Owner",
                column: "FlatID",
                principalTable: "Flat",
                principalColumn: "ID");
        }
    }
}
