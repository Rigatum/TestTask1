using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask1.Migrations
{
    /// <inheritdoc />
    public partial class FlatsOnlyOneOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatOwner");

            migrationBuilder.AddColumn<int>(
                name: "FlatID",
                table: "Owner",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owner_FlatID",
                table: "Owner",
                column: "FlatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Owner_Flat_FlatID",
                table: "Owner",
                column: "FlatID",
                principalTable: "Flat",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owner_Flat_FlatID",
                table: "Owner");

            migrationBuilder.DropIndex(
                name: "IX_Owner_FlatID",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "FlatID",
                table: "Owner");

            migrationBuilder.CreateTable(
                name: "FlatOwner",
                columns: table => new
                {
                    FlatsID = table.Column<int>(type: "integer", nullable: false),
                    OwnersID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatOwner", x => new { x.FlatsID, x.OwnersID });
                    table.ForeignKey(
                        name: "FK_FlatOwner_Flat_FlatsID",
                        column: x => x.FlatsID,
                        principalTable: "Flat",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlatOwner_Owner_OwnersID",
                        column: x => x.OwnersID,
                        principalTable: "Owner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlatOwner_OwnersID",
                table: "FlatOwner",
                column: "OwnersID");
        }
    }
}
