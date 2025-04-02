using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagment.Migrations
{
    /// <inheritdoc />
    public partial class FixIdentityOnAppointmentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 4, 3, 17, 35, 8, 758, DateTimeKind.Local).AddTicks(418));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 4, 17, 35, 8, 758, DateTimeKind.Local).AddTicks(429));

            migrationBuilder.UpdateData(
                table: "Clinics",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastSync",
                value: new DateTime(2025, 4, 2, 17, 35, 8, 757, DateTimeKind.Local).AddTicks(9967));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 4, 1, 17, 35, 8, 758, DateTimeKind.Local).AddTicks(467));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 2, 17, 35, 8, 758, DateTimeKind.Local).AddTicks(474));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 4, 3, 17, 27, 8, 481, DateTimeKind.Local).AddTicks(9498));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 4, 17, 27, 8, 481, DateTimeKind.Local).AddTicks(9510));

            migrationBuilder.UpdateData(
                table: "Clinics",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastSync",
                value: new DateTime(2025, 4, 2, 17, 27, 8, 481, DateTimeKind.Local).AddTicks(8871));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 4, 1, 17, 27, 8, 481, DateTimeKind.Local).AddTicks(9579));

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 2, 17, 27, 8, 481, DateTimeKind.Local).AddTicks(9587));
        }
    }
}
