using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbPlayground.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Equations",
                columns: new[] { "Id", "Calculation", "Result" },
                values: new object[] { 1, "5+5", 10 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Equations",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
