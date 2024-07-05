using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "LeaveRequestId",
                keyValue: 1,
                column: "DateNow",
                value: new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "LeaveRequestId",
                keyValue: 2,
                column: "DateNow",
                value: new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "LeaveRequestId",
                keyValue: 3,
                column: "DateNow",
                value: new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "LeaveRequestId",
                keyValue: 1,
                column: "DateNow",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "LeaveRequestId",
                keyValue: 2,
                column: "DateNow",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LeaveRequests",
                keyColumn: "LeaveRequestId",
                keyValue: 3,
                column: "DateNow",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
