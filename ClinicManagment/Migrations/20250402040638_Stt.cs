using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagment.Migrations
{
    /// <inheritdoc />
    public partial class Stt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 4, 3, 6, 6, 36, 245, DateTimeKind.Local).AddTicks(774));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 4, 6, 6, 36, 245, DateTimeKind.Local).AddTicks(787));

            migrationBuilder.UpdateData(
                table: "Clinics",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastSync",
                value: new DateTime(2025, 4, 2, 6, 6, 36, 245, DateTimeKind.Local).AddTicks(330));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 4, 1, 6, 6, 36, 245, DateTimeKind.Local).AddTicks(825));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 2, 6, 6, 36, 245, DateTimeKind.Local).AddTicks(830));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 4, 2, 21, 10, 32, 735, DateTimeKind.Local).AddTicks(7172));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 3, 21, 10, 32, 735, DateTimeKind.Local).AddTicks(7187));

            migrationBuilder.UpdateData(
                table: "Clinics",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastSync",
                value: new DateTime(2025, 4, 1, 21, 10, 32, 735, DateTimeKind.Local).AddTicks(6561));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 3, 31, 21, 10, 32, 735, DateTimeKind.Local).AddTicks(7252));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 1, 21, 10, 32, 735, DateTimeKind.Local).AddTicks(7260));
        }
    }
}
