using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortfolioApi.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                column: "ConcurrencyStamp",
                value: "42a68fc7-7ade-4de8-9600-021d24f1ed2c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                column: "ConcurrencyStamp",
                value: "6d3fd674-3e2f-49d1-a67f-f3c1a0afc9f9");

            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "Id", "Course", "EndDate", "School", "SectionId", "StartDate" },
                values: new object[,]
                {
                    { 1, "Full-stack developer", null, "VDAB", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Front-end developer", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BeCode", 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "DTP-Prepress", new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VDAB", 2, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Chemistry", new DateTime(2009, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AP Hoge School", 2, new DateTime(2007, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "WorkExperiences",
                columns: new[] { "Id", "Company", "EndDate", "Position", "SectionId", "StartDate" },
                values: new object[,]
                {
                    { 1, "ZAPFLOOR", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Front-End Developer", 3, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Gazelle Printing House", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prepress Coordinator", 3, new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Umicore", new DateTime(2009, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lab Technician", 3, new DateTime(2007, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Educations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Educations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Educations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Educations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WorkExperiences",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkExperiences",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkExperiences",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                column: "ConcurrencyStamp",
                value: "7345e2b2-d5c3-4315-ad9f-73041a6bb4c9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                column: "ConcurrencyStamp",
                value: "14ef0f04-3943-44a9-9c8c-d0e5da9c19b5");
        }
    }
}
