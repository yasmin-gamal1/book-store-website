using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d690e545-a26e-4ae6-a265-8fe7ce2fb1ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da1a9ba3-6492-4ba9-ab17-9c093423d540");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6b6ca9cc-2a57-4ea9-a911-038e78288d19", null, "customer", "CUSTOMER" },
                    { "b4864465-8732-4c17-8a64-c95a79d64035", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b6ca9cc-2a57-4ea9-a911-038e78288d19");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4864465-8732-4c17-8a64-c95a79d64035");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d690e545-a26e-4ae6-a265-8fe7ce2fb1ba", null, "admin", "ADMIN" },
                    { "da1a9ba3-6492-4ba9-ab17-9c093423d540", null, "customer", "CUSTOMER" }
                });
        }
    }
}
