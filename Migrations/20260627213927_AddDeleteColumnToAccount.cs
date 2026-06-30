using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBankingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteColumnToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Accounts");
        }
    }
}
