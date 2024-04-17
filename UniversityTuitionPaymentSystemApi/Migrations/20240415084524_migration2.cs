using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityTuitionPaymentSystemApi.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "PaymentDate", "StudentId", "Term" },
                values: new object[,]
                {
                    { 1, 2000f, new DateTime(2024, 4, 15, 8, 45, 22, 810, DateTimeKind.Utc).AddTicks(8828), 1, "1" },
                    { 2, 2000f, new DateTime(2024, 4, 15, 8, 45, 22, 810, DateTimeKind.Utc).AddTicks(8831), 2, "1" }
                });

            migrationBuilder.InsertData(
                table: "Tuitions",
                columns: new[] { "Id", "Amount", "StudentId", "Term" },
                values: new object[,]
                {
                    { 1, 2000f, 1, "1" },
                    { 2, 4000f, 2, "1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tuitions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tuitions",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
