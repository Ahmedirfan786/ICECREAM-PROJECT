using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Icecream.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedaddresscolumntoorderstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAdress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAdress",
                table: "Orders");
        }
    }
}
