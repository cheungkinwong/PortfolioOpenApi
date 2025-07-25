using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioApi.Migrations
{
    /// <inheritdoc />
    public partial class seedpasswordhash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "10e5c194-e911-4125-b924-264754278901", "AQAAAAEAACcQAAAAEHPfA2XinY6qHZPThr97lr0vrjBjXJbgO5XvvBbuS0Q++VU3FDR6NgQ4c7WpruOzNg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6a339106-0641-421d-a7aa-5fefa470faa4", "AQAAAAEAACcQAAAAEOG+srRNrPTnR4y2hvBowdppe2k6JMzU3OmGQoPu/zYtPR7KQWIRFwIGc8i6uY/QGQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "42a68fc7-7ade-4de8-9600-021d24f1ed2c", "$2b$12$UHXqd4tw92f.Tl1Ybp1hAuxOMoDoG.SMmgSlW7bgxQxRL4HzTm4Ei" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6d3fd674-3e2f-49d1-a67f-f3c1a0afc9f9", "$2b$12$CHlyLjz2jhDsSQ8knDmnMuEHtd4l3U8dGksLOeqepafHpJUuet9WO" });
        }
    }
}
