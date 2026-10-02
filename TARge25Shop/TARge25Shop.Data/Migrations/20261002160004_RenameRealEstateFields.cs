using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TARge25Shop.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameRealEstateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomNumber",
                table: "RealEstates",
                newName: "NrOfRooms");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "RealEstates",
                newName: "AreaCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NrOfRooms",
                table: "RealEstates",
                newName: "RoomNumber");

            migrationBuilder.RenameColumn(
                name: "AreaCode",
                table: "RealEstates",
                newName: "Area");
        }
    }
}
